using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParkingManager : MonoBehaviour
{
    [Header("Gameplay")]
    public Transform player;
    public Rigidbody playerRb;
    public List<ParkingSpot> spotsInOrder;

    [Header("Arrow")]
    public GameObject arrow;
    public float arrowHeight = 0.01f;
    public float arrowBobAmplitude = 0.2f;
    public float arrowBobSpeed = 2f;

    [Header("UI")]
    public GameObject winPanel;
    public GameObject gameOverPanel;

    private int currentIndex = 0;
    private bool gameEnded = false;
    private Vector3 arrowBasePos; // Fixed position above current spot

    void Start()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (!playerRb && player) playerRb = player.GetComponent<Rigidbody>();

        HidePanels();
        Time.timeScale = 1f;

        if (arrow) arrow.SetActive(true);

        // Set initial arrow position above first spot
        PositionArrow();
    }

    void Update()
    {
        if (gameEnded || spotsInOrder.Count == 0) return;

        var spot = spotsInOrder[currentIndex];

        // Check if player parked correctly
        if (spot.IsParked(player, playerRb, Time.deltaTime))
        {
            AdvanceSpot();
            return;
        }

        // Bob the arrow on top of fixed base position
        UpdateArrow();
    }

    void AdvanceSpot()
    {
        currentIndex++;
        if (currentIndex >= spotsInOrder.Count)
            WinGame();
        else
            PositionArrow(); // Move arrow to next spot
    }

    void PositionArrow()
    {
        if (!arrow) return;

        // Set base position only when spot changes
        arrowBasePos = spotsInOrder[currentIndex].transform.position + Vector3.up * arrowHeight;
        arrow.transform.position = arrowBasePos;

        // Optional: make arrow point toward player
        if (player)
            arrow.transform.LookAt(new Vector3(player.position.x, arrow.transform.position.y, player.position.z));
    }

    void UpdateArrow()
    {
        if (!arrow) return;

        float bob = Mathf.Sin(Time.time * Mathf.PI * 2f * arrowBobSpeed) * arrowBobAmplitude;
        Vector3 pos = arrowBasePos;
        pos.y += bob;
        arrow.transform.position = pos;

        if (player)
            arrow.transform.LookAt(new Vector3(player.position.x, arrow.transform.position.y, player.position.z));
    }

    public void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (arrow) arrow.SetActive(false);
        Show(gameOverPanel);

        Time.timeScale = 0f;
    }

    void WinGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (arrow) arrow.SetActive(false);
        Show(winPanel);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void HidePanels()
    {
        if (winPanel) winPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    void Show(GameObject panel)
    {
        if (panel) panel.SetActive(true);
    }
}
