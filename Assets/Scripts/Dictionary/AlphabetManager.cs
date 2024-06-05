using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetManager : MonoBehaviour
{
    public GameObject[] alphabetPanels;
    public Button[] alphabetButtons;

    private int activePanelIndex = 0;
    private Button previousButton;

    private void Start()
    {
        for (int i = 0; i < alphabetPanels.Length; i++)
        {
            alphabetPanels[i].SetActive(i == 0);
        }

        alphabetButtons[0].interactable = false;
        previousButton = alphabetButtons[0];

        for (int i = 0; i < alphabetButtons.Length; i++)
        {
            int index = i;
            alphabetButtons[i].onClick.AddListener(() => OnAlphabetButtonClicked(index));
        }
    }

    private void OnAlphabetButtonClicked(int index)
    {
        for (int i = 0; i < alphabetPanels.Length; i++)
        {
            alphabetPanels[i].SetActive(false);
        }

        if (index >= 0 && index < alphabetPanels.Length)
        {
            alphabetPanels[index].SetActive(true);
        }

        if (previousButton != null)
        {
            previousButton.interactable = true;
        }

        alphabetButtons[index].interactable = false;
        previousButton = alphabetButtons[index];
    }
}
