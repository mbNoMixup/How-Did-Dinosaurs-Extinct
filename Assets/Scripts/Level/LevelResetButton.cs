using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetButton : MonoBehaviour
{
    public LevelMenu levelMenu;

    public void ResetUnlockedLevels()
    {
        PlayerPrefs.SetInt("HighestUnlockedLevel", 5);
        PlayerPrefs.Save();
        Debug.Log("RESETED LEVELS");

        if (levelMenu != null)
        {
            levelMenu.UpdateLevelButtons();
        }
        else
        {
            Debug.LogWarning("LEVEL MENU NOT SET");
        }
    }
}