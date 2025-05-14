pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps { checkout scm }
        }
        stage('Build Backend') {
            steps {
                sh 'cd backend && pip install -r requirements.txt'
            }
        }
        stage('Test Backend') {
            steps {
                sh 'cd backend/tests && pytest'
            }
        }
        stage('Build Frontend') {
            steps {
                bat 'cd frontend\\AnomalyDetector && dotnet build'
            }
        }
        stage('Test Frontend') {
            steps {
                bat 'cd frontend\\AnomalyDetector && dotnet test'
            }
        }
        stage('Build Docker Image') {
            steps {
                sh 'cd backend && docker build -t anomaly-backend .'
            }
        }
        stage('Deploy to VM') {
            steps {
                sshPublisher(
                    publishers: [
                        sshPublisherDesc(
                            configName: 'vm-server',
                            transfers: [
                                sshTransfer(
                                    sourceFiles: '**/build/**/*',
                                    remoteDirectory: '/var/www/anomaly-app',
                                    removePrefix: 'build'
                                )
                            ]
                        )
                    ]
                )
            }
        }
    }
}
