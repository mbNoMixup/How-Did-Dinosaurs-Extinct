using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    
    // void Update()
    // {
    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         LoadNextLevel();
    //     }
    // }

    public void LoadNextLevel(int buildIndex)
    {
        StartCoroutine(LoadLevel(buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}

