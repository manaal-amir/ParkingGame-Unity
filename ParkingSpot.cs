using UnityEngine;

public class ParkingSpot : MonoBehaviour
{
    [Header("Park Requirements")]
    public float maxCenterOffset = 1.2f;
    public float maxSpeed = 0.35f;
    public float requiredStillTime = 0.8f;

    private float stillTimer = 0f;

    public bool IsParked(Transform car, Rigidbody carRb, float dt)
    {
        if (!car || !carRb) return false;

        // Check distance to center
        Vector3 carXZ = new Vector3(car.position.x, 0, car.position.z);
        Vector3 spotXZ = new Vector3(transform.position.x, 0, transform.position.z);
        float offset = Vector3.Distance(carXZ, spotXZ);

        if (offset > maxCenterOffset)
        {
            stillTimer = 0f;
            return false;
        }

        // Check speed
        if (carRb.linearVelocity.magnitude > maxSpeed)
        {
            stillTimer = 0f;
            return false;
        }

        // Accumulate still time
        stillTimer += dt;
        return stillTimer >= requiredStillTime;
    }
}
