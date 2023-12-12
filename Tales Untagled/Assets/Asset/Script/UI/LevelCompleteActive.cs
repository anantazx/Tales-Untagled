using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteActive : MonoBehaviour
{
    [SerializeField] private GameObject gameObjects;

    private void Start()
    {
        
    }

    private void Update()
    {
        string gameEndActive = ((Ink.Runtime.StringValue)DialogueManager.GetInstance().GetVariableState("ActiveEndGame")).value;

        switch (gameEndActive)
        {
            case "":
                gameObjects.SetActive(false);
                break;
            case "Open":
                gameObjects.SetActive(true);
                break;
            case "Close":
                gameObjects.SetActive(false);
                break;
            default:
                Debug.LogWarning("EndGame not Handled By Switch Statement: " + gameEndActive);
                break;
        }
    }
}
