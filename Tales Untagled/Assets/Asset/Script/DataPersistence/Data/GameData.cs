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



    public GameData()
    {
        this.Coins = 0;
        paperRollCollected = new SerializableDictionary<string, bool>();
    }
}
