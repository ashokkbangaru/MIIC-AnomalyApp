#!/bin/bash

# Update packages
sudo apt-get update
sudo apt-get install -y docker.io git

# Clone repository
git clone https://github.com/your-repo/AnomalyApp.git

# Navigate to the backend and build the Docker image
cd AnomalyApp/backend
docker build -t anomaly-backend .

# Run the Docker container
docker run -d -p 8000:8000 anomaly-backend
