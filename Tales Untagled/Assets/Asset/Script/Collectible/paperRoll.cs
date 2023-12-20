using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paperRoll : MonoBehaviour, IDataPersistence
{

    [SerializeField] private string id;
    [SerializeField] private bool collected = false;

    [ContextMenu("Generate Guid For ID")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    
    public void loadData(GameData data)
    {
        data.paperRollCollected.TryGetValue(id, out collected);
        if (collected)
        {
            Destroy(gameObject);
        }
    }

    public void saveData(GameData data)
    {
        if (data.paperRollCollected.ContainsKey(id))
        {
            data.paperRollCollected.Remove(id);
        }
        data.paperRollCollected.Add(id, collected);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                onPaperRollCollect();
            }
        }
       
    }


    private void onPaperRollCollect()
    {
        ItemCollector.Instance.CollectedPaperRolls();
        collected = true;
        Destroy(gameObject);
    }
}
