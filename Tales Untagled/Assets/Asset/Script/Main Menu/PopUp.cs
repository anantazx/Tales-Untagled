using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PopUp : Menu
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    [SerializeField] private GameObject saveSlotManager;

    public void ActivatedMenu(string displayText, UnityAction confirmAction, UnityAction cancelAction)
    {

        this.gameObject.SetActive(true);

        // menentukan display text
        this.displayText.text = displayText;

        // remove semua listeners yang ada untuk memastikan tidak ada sebelumnya yang masing ada pada listener
        // note - ini hanya meremove listeners yang di isikan oleh code
        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            DeactivateMenu();
            confirmAction();
        });

        cancelButton.onClick.AddListener(() =>
        {
            DeactivateMenu();
            cancelAction();
        });
    }

    private void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

}
