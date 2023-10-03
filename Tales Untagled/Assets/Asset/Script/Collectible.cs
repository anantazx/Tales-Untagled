using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI CoinsText;

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            
        }
    }

}
