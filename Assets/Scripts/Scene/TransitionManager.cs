using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject startingSceneTransition;
    [SerializeField] private GameObject endingSceneTransition;
    [SerializeField] private float startingSceneTransitionDuration = 5f;
    [SerializeField] private float endingSceneTransitionDuration = 5f; 

    private void Start()
    {
        if (startingSceneTransition != null)
        {
            startingSceneTransition.SetActive(true);
            Invoke("DisableStartingSceneTransition", startingSceneTransitionDuration);
        }
    }

    private void DisableStartingSceneTransition()
    {
        if (startingSceneTransition != null)
        {
            startingSceneTransition.SetActive(false);
        }
    }

    public void LoadNextLevel(string nextLevelName)
    {
        StartCoroutine(LoadNextLevelCoroutine(nextLevelName));
    }

    private IEnumerator LoadNextLevelCoroutine(string nextLevelName)
    {
        if (endingSceneTransition != null)
        {
            endingSceneTransition.SetActive(true);
            yield return new WaitForSeconds(endingSceneTransitionDuration);
        }

        SceneManager.LoadScene(nextLevelName);
    }
}
