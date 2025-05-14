import os
import numpy as np
from PIL import Image
import tensorflow as tf

def load_labels(labels_path):
    with open(labels_path, "r") as f:
        return [line.strip() for line in f.readlines()]

def preprocess_image(image_path):
    image = Image.open(image_path).convert("RGB")
    image = image.resize((224, 224))
    image = np.array(image).astype(np.float32) / 255.0
    return np.expand_dims(image, axis=0)  # shape: (1, 224, 224, 3)

def run_inference(image_path, model_path, labels_path):
    assert os.path.exists(image_path), f"Image file missing: {image_path}"
    assert os.path.exists(model_path), "Model file missing"
    assert os.path.exists(labels_path), "Labels file missing"

    labels = load_labels(labels_path)
    image = preprocess_image(image_path)

    # Load model
    interpreter = tf.lite.Interpreter(model_path=model_path)
    interpreter.allocate_tensors()

    input_details = interpreter.get_input_details()
    output_details = interpreter.get_output_details()

    # Set input tensor and run inference
    interpreter.set_tensor(input_details[0]['index'], image)
    interpreter.invoke()

    # Get output and make prediction
    output = interpreter.get_tensor(output_details[0]['index'])
    prediction_index = int(np.argmax(output))  # Get index of max output
    predicted_label = labels[prediction_index]  # Map index to label

    return predicted_label

def test_inference_on_images():
    model_path = os.path.join("backend", "model", "model_unquant.tflite")
    labels_path = os.path.join("backend", "model", "labels.txt")

    # Test images
    test_images = ["test_normal.jpg", "test_abnormal.jpg"]

    for test_image in test_images:
        test_image_path = os.path.join("backend", "tests", "test_images", test_image)

        predicted_label = run_inference(test_image_path, model_path, labels_path)
        print(f"✅ Image: {test_image} - Predicted: {predicted_label}")

        # Assert that the predicted label is in the list of labels (you can modify this as per your logic)
        assert predicted_label in load_labels(labels_path), "Prediction not in label list"

if __name__ == "__main__":
    test_inference_on_images()