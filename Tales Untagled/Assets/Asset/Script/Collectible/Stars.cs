using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int id;
    [SerializeField] private bool collected = false;
    
    

    [ContextMenu("Generate Guid for ID")]
    private void GenerateGUID()
    {
        id = System.Guid.NewGuid().GetHashCode();
    }

    public void loadData(GameData data)
    {
        int convertedID = Mathf.Abs(id);
        int collectedValue;
        
        data.levelStarsCollected.TryGetValue(convertedID, out collectedValue);
        collected = collectedValue == 1;
        if (collected)
        {
            Destroy(gameObject);
        }
    }

    public void saveData(GameData data)
    {
        int convertedID = Mathf.Abs(id);

        if (data.levelStarsCollected.ContainsKey(convertedID))
        {
            data.levelStarsCollected.Remove(convertedID);
        }
        data.levelStarsCollected.Add(convertedID, collected ? 1 : 0 ) ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OnStarCollected();
            }
        }
    }

    private void OnStarCollected()
    {
        ItemCollector.Instance.StarsCollectedCount();
        collected = true;
        Destroy(gameObject);
    }

}
