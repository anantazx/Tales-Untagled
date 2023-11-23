using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    // values yang diberikan pada constuctor ini akan berupa default values
    // games nya dimulai jika tidak ada data untuk di load
    public int deathCount;
    public int stars;
    public SerializableDictionary<int, int> levelStarsCollected;
    public SerializableDictionary<int, bool> unlockedLevels;
    public SerializableDictionary<string, bool> paperRollCollected;


    public long lastUpdated;

    public GameData()
    {
        this.deathCount = 0;
        this.stars = 0;
        paperRollCollected = new SerializableDictionary<string, bool>();
        unlockedLevels = new SerializableDictionary<int, bool>();
        levelStarsCollected = new SerializableDictionary<int, int>();
    }

    public int GetpercetageComplete()
    {
        // sistem memikirkan beberapa banyak paperRoll yang dikumpulkan
        int totalCollected = 0;
        foreach (bool collected in paperRollCollected.Values)
        {
            if (collected)
            {
                totalCollected++;
            }
        }

        // memastikan kita tidak membagi dengan 0 ketika mengkalkulasi persennya
        int PercentageCompleted = -1;
        if (paperRollCollected.Count != 0)
        {
            PercentageCompleted = (totalCollected * 100 / paperRollCollected.Count);
        }
        return PercentageCompleted;
    }

}
