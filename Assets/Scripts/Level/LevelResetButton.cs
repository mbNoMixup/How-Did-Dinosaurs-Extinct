using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetButton : MonoBehaviour
{
    public LevelMenu levelMenu;

    public void ResetUnlockedLevels()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.SetInt("ReachedIndex", 1);
        PlayerPrefs.Save();
        Debug.Log("Unlocked Levels reset to 1");

        if (levelMenu != null)
        {
            levelMenu.UpdateLevelButtons();
        }
        else
        {
            Debug.LogWarning("LevelMenu reference is not set");
        }
    }
}
