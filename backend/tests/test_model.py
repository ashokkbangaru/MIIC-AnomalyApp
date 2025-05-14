import os
import tensorflow as tf
import numpy as np

def test_model_load():
    model_path = os.path.join("backend", "model", "model_unquant.tflite")
    assert os.path.exists(model_path), f"Model not found at {model_path}"

    # Try loading the TFLite model
    interpreter = tf.lite.Interpreter(model_path=model_path)
    interpreter.allocate_tensors()

    input_details = interpreter.get_input_details()
    output_details = interpreter.get_output_details()

    # Check input shape using np.array_equal() for array comparison
    assert np.array_equal(input_details[0]['shape'], [1, 224, 224, 3]), "Input shape mismatch"
    assert output_details[0]['shape'][0] == 1, "Output batch size mismatch"

    print("✅ test_model_load passed")

if __name__ == "__main__":
    test_model_load()