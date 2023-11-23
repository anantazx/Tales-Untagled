using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour, IDataPersistence
{

    public static ItemCollector Instance { get; private set; }


    private int deathCount = 0;
    private int PaperRolls = 0;
    private int stars = 0;
    [SerializeField] private TextMeshProUGUI deathCounterText;
    [SerializeField] private TextMeshProUGUI paperRollsText;
    [SerializeField] private TextMeshProUGUI starsText;
    

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Found more than one Item Collector in the scene");
            Destroy(gameObject);
        }
        Instance = this;
    }



    public void loadData(GameData data)
    {
        this.deathCount = data.deathCount;

        foreach (KeyValuePair<string, bool> pair in data.paperRollCollected)
        {
            if (pair.Value)
            {
                PaperRolls++;
            }
        }

        int currentLevelStars = 0;
        if (data.levelStarsCollected.ContainsKey(LevelSelectManager.instance.currentLevel))
        {
            currentLevelStars = data.levelStarsCollected[LevelSelectManager.instance.currentLevel];
        }
        stars = currentLevelStars;

    }

    public void saveData(GameData data)
    {
        data.deathCount = this.deathCount;
    }



    private void Update()
    {
        deathCounterText.text = "X " + deathCount;
        paperRollsText.text = "Paper Rolls : " + PaperRolls;
        starsText.text = stars + " /3" ;
    }


    public void CollectedPaperRolls()
    {
        PaperRolls++;
        
    }

    public void PlayerDeathCount()
    {
        deathCount++;
    }

    public void StarsCollectedCount()
    {
        stars++;
        DataPersistanceManager.instance.CollectStar(LevelSelectManager.instance.currentLevel,stars);
    }

    public void RestartStarCount()
    {
        stars = 0;
    }

    public int GetDeathCount()
    {
        return deathCount;
    }

}
