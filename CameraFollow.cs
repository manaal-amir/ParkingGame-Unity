using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // Drag your car here
    public Vector3 offset = new Vector3(0, 5, -10); // Camera position relative to car
    public float followSpeed = 5f;   // Smoothness of following

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Optional: make the camera look at the car
        transform.LookAt(target.position);
    }
}
