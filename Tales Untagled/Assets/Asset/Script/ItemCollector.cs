using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour, IDataPersistence
{

    public static ItemCollector Instance { get; private set; }


    private int Coins = 0;
    private int PaperRolls = 0;
    [SerializeField] private TextMeshProUGUI CoinsText;
    [SerializeField] private TextMeshProUGUI paperRollsText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Item Collector in the scene");
        }
        Instance = this;
    }



    public void loadData(GameData data)
    {
        this.Coins = data.Coins;

        foreach (KeyValuePair<string, bool> pair in data.paperRollCollected)
        {
            if (pair.Value)
            {
                PaperRolls++;
            }
        }

    }

    public void saveData(ref GameData data)
    {
        data.Coins = this.Coins;
    }

    private void Update()
    {
        CoinsText.text = "COINS : " + Coins;
        paperRollsText.text = "Paper Rolls : " + PaperRolls;
    }

    public void colllectedCoins()
    {
        Coins++;
        CoinsText.text = "COINS : " + Coins;
    }

    public void CollectedPaperRolls()
    {
        PaperRolls++;
        paperRollsText.text = "Paper Rolls : " + PaperRolls;
    }


}
