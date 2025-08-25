using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("My Game"); // Replace with your main scene name
    }

    public void QuitGame()
    {
        Application.Quit(); // Works in build
    }
}
