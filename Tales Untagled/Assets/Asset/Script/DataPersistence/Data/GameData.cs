using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    // values yang diberikan pada constuctor ini akan berupa default values
    // games nya dimulai jika tidak ada data untuk di load
    public int Coins;

    public SerializableDictionary<string, bool> paperRollCollected;

    public long lastUpdated;

    public GameData()
    {
        this.Coins = 0;
        paperRollCollected = new SerializableDictionary<string, bool>();
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
