using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Button nextLevelButton;
    public Button levelSelectButton;

    void Start()
    {
        string currentScene = PlayerPrefs.GetString("LastLevel", "Level1");
        string nextLevel = "";

        if (currentScene == "Level1") nextLevel = "Level2";
        else if (currentScene == "Level2") nextLevel = "Level3";

        bool hasNextLevel = !string.IsNullOrEmpty(nextLevel) &&
                            PlayerPrefs.GetInt($"{nextLevel}_Unlocked", 0) == 1;

        nextLevelButton.interactable = hasNextLevel;

        nextLevelButton.onClick.AddListener(() =>
        {
            if (hasNextLevel)
                SceneManager.LoadScene(nextLevel);
        });

        levelSelectButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LevelSelect");
        });
    }
}