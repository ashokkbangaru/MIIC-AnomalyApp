# 🚀 AnomalyApp

AnomalyApp is a desktop-based image anomaly detection tool combining a **C# Windows Forms frontend** and a **FastAPI + TensorFlow Lite backend**.

## 🧩 Features
- Image classification using TensorFlow Lite
- FastAPI backend with Docker support
- C# frontend with plugin architecture
- CI/CD using GitHub Actions and Jenkins
- Deployable on Oracle VirtualBox VMs

## 🗂️ Project Structure
```
AnomalyApp/
├── backend/               # FastAPI + TensorFlow backend
│   ├── main.py
│   ├── model/
│   ├── requirements.txt
│   ├── Dockerfile
│   └── tests/
├── frontend/              # C# WinForms frontend
│   ├── AnomalyDetector/
        ├── appsettings.json
│   ├── PluginContracts/
│   ├── SamplePlugin/
│   ├── Plugins/
│   └── AnomalyDetector.sln
├── deployment/            # Oracle VM setup script
│   └── setup_vm.sh
├── .github/
│   └── workflows/
│       └── ci.yml
├── Jenkinsfile
└── README.md
```

## 🔧 Local Development

### Backend (FastAPI)
```bash
cd backend
python -m venv venv && source venv/bin/activate  # On Windows: venv\Scripts\activate
pip install -r requirements.txt
uvicorn main:app --reload
```

### Frontend (C#)
- Open `AnomalyDetector.sln` in Visual Studio
- Build and run the solution
- Ensure `appsettings.json` has:
```json
{
  "AppSettings": {
    "BackendUrl": "http://127.0.0.1:8000"
  }
}
```

### Testing
```bash
cd backend
pytest tests/
```

## 🐳 Docker (Backend)
```bash
cd backend
docker build -t anomaly-backend .
docker run -p 8000:8000 anomaly-backend
```

## 🚀 Deployment (Oracle VM)
```bash
cd deployment
chmod +x setup_vm.sh
./setup_vm.sh
```

## ✅ CI/CD
- GitHub Actions: `.github/workflows/ci.yml`
- Jenkins: see `Jenkinsfile`

## Happy Coding!!
---
```
