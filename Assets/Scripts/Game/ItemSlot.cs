using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int id;
    public GameManager gameManager;
    [SerializeField] private AudioSource correctPlaceSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().id == id)
            {
                RectTransform puzzlePieceRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
                puzzlePieceRectTransform.SetParent(transform);
                puzzlePieceRectTransform.anchoredPosition = Vector2.zero;
                gameManager.PuzzlePiecePlaced();
                eventData.pointerDrag.GetComponent<DragAndDrop>().SetPlaced(true);
                eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 1f;

                PlaySound();
            }
            else
            {
                eventData.pointerDrag.GetComponent<DragAndDrop>().ResetPosition();
            }
        }
    }

    private void PlaySound()
    {
        if (correctPlaceSound != null)
        {
            correctPlaceSound.Play();
        }
    }

    public bool IsCorrectPuzzlePiece()
    {
        if (transform.childCount > 0)
        {
            DragAndDrop puzzlePiece = transform.GetChild(0).GetComponent<DragAndDrop>();
            return puzzlePiece != null && puzzlePiece.id == id;
        }
        return false;
    }
}
