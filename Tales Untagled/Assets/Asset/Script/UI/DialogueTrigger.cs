using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Trigger Hint")]
    [SerializeField] private GameObject TriggerHint;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset InkJSON;

    private bool PlayerInRange;

    private void Awake()
    {
        PlayerInRange = false;
        TriggerHint.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInRange && !DialogueManager.GetInstance().DialogueIsPlaying)
        {
            TriggerHint.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(InkJSON);
            }
        }
        else
        {
            TriggerHint.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }

}
