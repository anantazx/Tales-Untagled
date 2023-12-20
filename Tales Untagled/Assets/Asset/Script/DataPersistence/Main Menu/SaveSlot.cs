using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileID = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasdataContent;
    [SerializeField] private TextMeshProUGUI percentageCompletetion;
    [SerializeField] private TextMeshProUGUI paperRollCounter;

    [Header("Delete Button")]
    [SerializeField] private Button deleteButton;

    private Button saveSlotsButton;

    public bool hasData { get; private set; } = false;

    private void Awake()
    {
        saveSlotsButton = this.GetComponent<Button>();
    }

    public void SetData( GameData data)
    {
        if (data == null)
        {
            hasData = false;
            noDataContent.SetActive(true);
            hasdataContent.SetActive(false);
        }
        else
        {
            hasData = true;
            noDataContent.SetActive(false);
            hasdataContent.SetActive(true);

            percentageCompletetion.text = data.GetpercetageComplete() + "% Complete";
            // memfilter dictionary untuk menghitung values dari true
            int trueCount = data.paperRollCollected.Values.Count(value => value);
            // mendisplay trueCout menjadi string
            paperRollCounter.text = "Paper Rolls: " + trueCount.ToString();
        }
    }

    public string GetProfileID()
    {
        return this.profileID;
    } 

    public void SetInteractabel(bool interactable)
    {
        saveSlotsButton.interactable = interactable;
        deleteButton.interactable = interactable;
    }

}
