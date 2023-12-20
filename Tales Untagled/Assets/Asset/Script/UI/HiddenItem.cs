using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenItem : MonoBehaviour
{
    [SerializeField] private GameObject gameObject2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string gameErase = ((Ink.Runtime.StringValue)DialogueManager.GetInstance().GetVariableState("ShowHidden")).value;

        switch (gameErase)
        {
            case "":
                gameObject2.SetActive(true);
                break;
            case "Open":
                gameObject2.SetActive(false);
                break;
            case "Close":
                gameObject2.SetActive(true);
                break;
            default:
                Debug.LogWarning("EndGame not Handled By Switch Statement: " + gameErase);
                break;
        }
    }
}
