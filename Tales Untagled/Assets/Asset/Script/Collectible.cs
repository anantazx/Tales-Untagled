using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           onCoinCollected();
        }
    }

    private void onCoinCollected()
    {
        ItemCollector.Instance.colllectedCoins();
        Destroy(gameObject);
    }
   

}
