using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{

    public CharacterMovement Playermove;


    private int Coins = 0;
    [SerializeField] private TextMeshProUGUI CoinsText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            Coins++;
            CoinsText.text = "COINS : " + Coins;
            
        }

        

    }

  
}
