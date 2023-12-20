using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHidden : MonoBehaviour
{
    [SerializeField] private GameObject hiddenPlatform;

    private void Start()
    {

    }

    private void Update()
    {
        string gameEndActive = ((Ink.Runtime.StringValue)DialogueManager.GetInstance().GetVariableState("HiddenPlatform")).value;

        switch (gameEndActive)
        {
            case "":
                hiddenPlatform.SetActive(false);
                break;
            case "Open":
                hiddenPlatform.SetActive(true);
                break;
            case "Close":
                hiddenPlatform.SetActive(false);
                break;
            default:
                Debug.LogWarning("EndGame not Handled By Switch Statement: " + gameEndActive);
                break;
        }




    }
}
