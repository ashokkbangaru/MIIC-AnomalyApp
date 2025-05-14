from fastapi import FastAPI, UploadFile, File
import numpy as np
from PIL import Image
from io import BytesIO
import tensorflow as tf
import os
from fastapi.middleware.cors import CORSMiddleware  # Import CORS middleware

app = FastAPI()

# Add CORS middleware to allow requests from different origins (like frontend on a different port)
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # Allows all origins; you can specify a list of origins for more security
    allow_credentials=True,
    allow_methods=["*"],  # Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
    allow_headers=["*"],  # Allows all headers
)

BASE_DIR = os.path.dirname(os.path.abspath(__file__))
model_path = os.path.join(BASE_DIR, "model", "model_unquant.tflite")
labels_path = os.path.join(BASE_DIR, "model", "labels.txt")

if not os.path.exists(model_path):
    raise FileNotFoundError(f"Model file not found: {model_path}")

# Load TFLite model
interpreter = tf.lite.Interpreter(model_path=model_path)
interpreter.allocate_tensors()

# Get model input/output details
input_details = interpreter.get_input_details()
output_details = interpreter.get_output_details()

# Load labels
with open(labels_path, "r") as f:
    labels = [line.strip() for line in f.readlines()]

@app.post("/predict")
async def predict(file: UploadFile = File(...)):
    # Read the uploaded file
    contents = await file.read()
    image = Image.open(BytesIO(contents)).convert("RGB")
    image = image.resize((224, 224))  # Resize the image to the model's expected size
    image = np.array(image).astype(np.float32) / 255.0  # Normalize the image
    image = np.expand_dims(image, axis=0)  # Add batch dimension

    # Set the tensor for prediction
    interpreter.set_tensor(input_details[0]['index'], image)
    interpreter.invoke()

    # Get the output prediction
    output_data = interpreter.get_tensor(output_details[0]['index'])
    prediction_index = int(np.argmax(output_data))
    predicted_class = labels[prediction_index]

    # Return the prediction result as a JSON object
    return {"prediction": predicted_class}

