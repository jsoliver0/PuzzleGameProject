using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelect"); // Or whatever your next scene is named
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // Only shows in Editor
    }
}