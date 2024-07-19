using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinished : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevelSelection(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void NextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;

        int highestUnlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 5);

        if (nextLevelIndex <= 10)
        {
            if (nextLevelIndex <= highestUnlockedLevel)
            {
                LoadLevel(nextLevelIndex);
            }
            else
            {
                Debug.Log("NEXT LEVEL IS LOCKED");
            }
        }
        else
        {
            Debug.Log("NO MORE LEVELS");
        }
    }

    private void LoadLevel(int levelIndex)
    {
        string nextLevelName = SceneUtility.GetScenePathByBuildIndex(levelIndex);
        string nextLevelNameFormatted = System.IO.Path.GetFileNameWithoutExtension(nextLevelName);

        if (!string.IsNullOrEmpty(nextLevelNameFormatted))
        {
            SceneManager.LoadScene(nextLevelNameFormatted);
        }
        else
        {
            Debug.Log("LEVEL DOES NOT EXIST: " + nextLevelNameFormatted);
        }
    }
}