using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ItemSlot[] slots;
    [SerializeField]
    private GameObject puzzleFinishedObject;

    private void Start()
    {
        if (puzzleFinishedObject != null)
        {
            puzzleFinishedObject.SetActive(false);
        }
    }

    public void PuzzlePiecePlaced()
    {
        Debug.Log("PIECE PLACED");
        if (CheckAllPuzzlePiecesPlaced())
        {
            Debug.Log("ALL PIECES PLACED, CHECKING LEVEL");
            StartCoroutine(ShowPuzzleFinishedObject());
            CheckLevelFinished();

            if (CheckLevelFinished())
            {
                Debug.Log("LEVEL FINISHED");
                // Call your transition manager here if needed
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

    private IEnumerator ShowPuzzleFinishedObject()
    {
        yield return new WaitForSeconds(3f);
        if (puzzleFinishedObject != null)
        {
            puzzleFinishedObject.SetActive(true);
        }
    }
}
