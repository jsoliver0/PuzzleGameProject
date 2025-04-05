using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string currentScene = SceneManager.GetActiveScene().name;

            // Save progress
            PlayerPrefs.SetString("LastLevel", currentScene);

            if (currentScene == "Level1")
            {
                PlayerPrefs.SetInt("Level2_Unlocked", 1);
            }
            else if (currentScene == "Level2")
            {
                PlayerPrefs.SetInt("Level3_Unlocked", 1);
            }

            PlayerPrefs.Save();
            SceneManager.LoadScene("WinScreen");
        }
    }
}