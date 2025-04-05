using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    void Start()
    {
        level1Button.interactable = true;

        // Unlocks based on progress saved via PlayerPrefs
        level2Button.interactable = PlayerPrefs.GetInt("Level2_Unlocked", 0) == 1;
        level3Button.interactable = PlayerPrefs.GetInt("Level3_Unlocked", 0) == 1;

        level1Button.onClick.AddListener(() => LoadLevel("Level1"));
        level2Button.onClick.AddListener(() => LoadLevel("Level2"));
        level3Button.onClick.AddListener(() => LoadLevel("Level3"));
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}