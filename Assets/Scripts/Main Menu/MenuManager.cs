using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Go to Daily Choice Scene
    public void PlayGame()
    {
        SceneManager.LoadScene("DailyChoice");
    }

    // Quit the Application
    public void QuitGame()
    {
        Application.Quit();
    }
}
