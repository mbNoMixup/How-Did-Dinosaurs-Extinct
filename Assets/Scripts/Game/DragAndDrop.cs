using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip dropSound;
    private AudioSource audioSource;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initPos;
    public int id;
    private bool isPlaced = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();
        initPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isPlaced)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            PlaySound(pickupSound);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isPlaced)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isPlaced)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            PlaySound(dropSound);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }

    public void ResetPosition()
    {
        transform.position = initPos;
    }

    public void SetPlaced(bool placed)
    {
        isPlaced = placed;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}