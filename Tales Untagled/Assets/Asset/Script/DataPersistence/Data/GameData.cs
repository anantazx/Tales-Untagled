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
        foreach (int collected in levelStarsCollected.Values)
        {
            if (collected != 0)
            {
                totalCollected += collected;
            }
        }

        // memastikan kita tidak membagi dengan 0 ketika mengkalkulasi persennya
        int PercentageCompleted = -1;
        if (levelStarsCollected.Count != 0)
        {
            int totalPossibleCollected = levelStarsCollected.Count * 3; // misalkan maksimum yang bisa dikumpulkan adalah 3 bintang untuk setiap lagu
            PercentageCompleted = (totalCollected * 100 / totalPossibleCollected);
        }
        return PercentageCompleted;
    }

}
