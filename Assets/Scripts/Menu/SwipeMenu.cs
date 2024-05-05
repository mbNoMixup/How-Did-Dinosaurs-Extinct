using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform menuRectTransform;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshold;

    [SerializeField] Button previousButton, nextButton;
    bool isButtonClickable = true;

    public void Awake()
    {
        currentPage = 1;
        targetPos = menuRectTransform.localPosition;
        dragThreshold = Screen.width / 15;
        MovePage();
        UpdateArrowButton();
    }

    public void Next()
    {
        if (isButtonClickable && currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
            StartCoroutine(EnableButtonAfterDelay());
        }
    }

    public void Previous()
    {
        if (isButtonClickable && currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
            StartCoroutine(EnableButtonAfterDelay());
        }
    }

    IEnumerator EnableButtonAfterDelay()
    {
        isButtonClickable = false;
        yield return new WaitForSeconds(0.4f);
        isButtonClickable = true;
    }

    void MovePage()
    {
        menuRectTransform.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        
        for (int i = 0; i < menuRectTransform.childCount; i++)
        {
            RectTransform pageTransform = menuRectTransform.GetChild(i) as RectTransform;
            
            bool isCurrentPage = (i == currentPage - 1);
            
            CanvasGroup canvasGroup = pageTransform.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = pageTransform.gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = isCurrentPage ? 1.0f : 0.5f;
            
            foreach (Button button in pageTransform.GetComponentsInChildren<Button>())
            {
                button.interactable = isCurrentPage;
            }
        }

        UpdateArrowButton();
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

    void UpdateArrowButton()
    {
        nextButton.interactable = isButtonClickable && currentPage < maxPage;
        previousButton.interactable = isButtonClickable && currentPage > 1;
    }
}
