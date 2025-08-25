using UnityEngine;

public class PlayerCollisionLose : MonoBehaviour
{
    private ParkingManager manager;

    void Start()
    {
        manager = FindFirstObjectByType<ParkingManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (manager == null) return;

        if (collision.collider.CompareTag("ParkedCar") || collision.collider.CompareTag("Obstacle"))
        {
            manager.GameOver();
        }
    }
}
