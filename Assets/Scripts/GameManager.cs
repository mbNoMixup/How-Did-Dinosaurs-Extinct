using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ItemSlot[] slots;
    [SerializeField]
    private TextMeshProUGUI puzzleFinishedText;

    [SerializeField]
    private GameObject _startingSceneTransition;
    [SerializeField]
    private GameObject _endingSceneTransition;

    private void Start()
    {
        puzzleFinishedText.gameObject.SetActive(false);
        _startingSceneTransition.SetActive(true);
        Invoke("DisableStartingSceneTransition", 5f);
    }

    public void PuzzlePiecePlaced()
    {
        Debug.Log("PIECE PLACED");
        if (CheckAllPuzzlePiecesPlaced())
        {
            Debug.Log("ALL PIECES PLACED, CHECKING LEVEL");
            StartCoroutine(ShowPuzzleFinishedText());
            CheckLevelFinished();

            if (CheckLevelFinished())
            {
                Debug.Log("LEVEL FINISHED");
            }
            else
            {
                Debug.Log("LEVEL IS NOT FINISHED");
            }
        }
    }

    public bool CheckAllPuzzlePiecesPlaced()
    {
        foreach (ItemSlot slot in slots)
        {
            if (!slot.IsCorrectPuzzlePiece())
            {
                return false;
            }
        }
        return true;
    }

    public bool CheckLevelFinished()
    {
        Debug.Log("CHECKING LEVEL");
        
        foreach (ItemSlot slot in slots)
        {
            if (!slot.IsCorrectPuzzlePiece())
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator ShowPuzzleFinishedText()
    {
        yield return new WaitForSeconds(3f);
        puzzleFinishedText.gameObject.SetActive(true);
        puzzleFinishedText.text = "PUZZLE FINISHED!";
    }

    private void DisableStartingSceneTransition()
    {
        _startingSceneTransition.SetActive(false);
    }
    private void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "2")
        {
            SceneManager.LoadScene("3");
        }
        else
        {
            SceneManager.LoadScene("2");
        }
    }
}
