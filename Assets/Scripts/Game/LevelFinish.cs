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

        if (nextLevelIndex <= 6)
        {
            string nextLevelName = SceneUtility.GetScenePathByBuildIndex(nextLevelIndex);
            string nextLevelNameFormatted = System.IO.Path.GetFileNameWithoutExtension(nextLevelName);
            
            if (!string.IsNullOrEmpty(nextLevelNameFormatted))
            {
                SceneManager.LoadScene(nextLevelNameFormatted);
            }
            else
            {
                Debug.Log("Next level does not exist: " + nextLevelNameFormatted);
            }
        }
        else
        {
            Debug.Log("No more levels available.");
        }
    }
}