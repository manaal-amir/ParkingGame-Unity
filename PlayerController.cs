using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 10f;      // Forward/backward speed
    public float turnSpeed = 50f;      // Turning speed

    void Update()
    {
        // Get input
        float moveInput = Input.GetAxis("Vertical");   // W/S or Up/Down arrows
        float turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows

        // Move forward/backward
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);

        // Turn only if the car is moving forward or backward
        if (Mathf.Abs(moveInput) > 0.1f) // Car must be moving
        {
            float turnAmount = turnInput * turnSpeed * Time.deltaTime * Mathf.Sign(moveInput);
            transform.Rotate(Vector3.up, turnAmount);
        }
    }
}
