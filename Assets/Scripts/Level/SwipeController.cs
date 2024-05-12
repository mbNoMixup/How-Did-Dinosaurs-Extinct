using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshold;

    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;

    [SerializeField] Button previousButton, nextButton;

    public void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshold = Screen.width / 15;
        UpdateBar();
        UpdateArrowButton();
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        nextButton.interactable = false;
        previousButton.interactable = false;

        SetChildAlpha(previousButton, 0.5f);
        SetChildAlpha(nextButton, 0.5f);

        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType).setOnComplete(() =>
        {
            LeanTween.delayedCall(0.6f, () =>
            {
                UpdateArrowButton();
            });
        });

        UpdateBar();
    }

    void SetChildAlpha(Button button, float alpha)
    {
        foreach (Graphic graphic in button.GetComponentsInChildren<Graphic>())
        {
            graphic.canvasRenderer.SetAlpha(alpha);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x)
            {
                Previous();
            }
            else
            {
                Next();
            }
        }
        else
        {
            MovePage();
        }
    }

    void UpdateBar()
    {
        foreach (var item in barImage)
        {
            item.sprite = barClosed;
        }
        barImage[currentPage - 1].sprite = barOpen;
    }

    void UpdateArrowButton()
    {
        nextButton.interactable = true;
        previousButton.interactable = true;

        if (currentPage == 1)
        {
            previousButton.interactable = false;
            SetChildAlpha(previousButton, 0.5f);
            SetChildAlpha(nextButton, 1f);
        }
        else if (currentPage == maxPage)
        {
            nextButton.interactable = false;
            SetChildAlpha(nextButton, 0.5f);
            SetChildAlpha(previousButton, 1f);
        }
        else if (currentPage != 1 && currentPage != maxPage)
        {
            SetChildAlpha(nextButton, 1f);
            SetChildAlpha(previousButton, 1f);
        }
    }
}
