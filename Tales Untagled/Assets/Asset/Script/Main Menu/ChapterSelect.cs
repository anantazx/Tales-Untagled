using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSelect : Menu
{
    [Header("Chapter Select")]
    [SerializeField] private Button[] totalChapter;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField]private float scrollStep;


    void Start()
    {
        GameObject[] chapterGameObject = GameObject.FindGameObjectsWithTag("Chapter");

        totalChapter = new Button[chapterGameObject.Length];

        for (int i = 0; i < chapterGameObject.Length; i++)
        {
            totalChapter[i] = chapterGameObject[i].GetComponent<Button>();
        }

        scrollStep = 2f / totalChapter.Length;

        nextButton.onClick.AddListener(NextChapter);
        prevButton.onClick.AddListener(PrevChapter);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonInteractive();

    }

    private void UpdateButtonInteractive()
    {
        nextButton.interactable = scrollRect.horizontalNormalizedPosition <= 1;
        prevButton.interactable = scrollRect.horizontalNormalizedPosition >= 0;
    }

    private void ScrollToChapter(float normalizePosition)
    {
        scrollRect.horizontalNormalizedPosition = normalizePosition;
    }

    private void NextChapter()
    {
        ScrollToChapter(Mathf.Clamp(scrollRect.horizontalNormalizedPosition + scrollStep, 0f, 1f));
    }

    private void PrevChapter()
    {
        ScrollToChapter(Mathf.Clamp(scrollRect.horizontalNormalizedPosition - scrollStep, 0f, 1f));
    }


    private void ChapterSelectArrayShown()
    {
        
    }
}
