using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            SetTextAlpha(buttons[i], 0.5f);
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
            SetTextAlpha(buttons[i], 1f);
        }
    }

    void SetTextAlpha(Button button, float alpha)
    {
        foreach (TextMeshProUGUI text in button.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.canvasRenderer.SetAlpha(alpha);
        }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
