pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build Backend') {
            steps {
                bat 'cd backend && pip install -r requirements.txt'
            }
        }

        stage('Test Backend') {
            steps {
                bat 'cd backend\\tests && pytest'
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
            when {
                expression { return env.SKIP_DOCKER != 'true' }
            }
            steps {
                bat 'cd backend && docker build -t anomaly-backend .'
            }
        }

        stage('Deploy to VM') {
            when {
                expression { return env.SKIP_DEPLOYMENT != 'true' }
            }
            steps {
                echo 'Skipping actual deployment for now.'
                // Add SSH deploy config when ready
            }
        }
    }
}
