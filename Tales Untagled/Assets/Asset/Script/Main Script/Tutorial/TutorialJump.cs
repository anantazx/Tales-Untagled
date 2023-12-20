using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialJump : MonoBehaviour
{
 
    
    [SerializeField] private GameObject tutorialsJump;
    [SerializeField] private GameObject triggerHint;
    [SerializeField] private bool cantMove;
    private bool isTutorialRunning = false;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && isTutorialRunning)
        {
            StartCoroutine(ShowTutorialRun());
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerHint.SetActive(true);
            isTutorialRunning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerHint.SetActive(false);
            isTutorialRunning = false;
        }
    }

    private IEnumerator ShowTutorialRun()
    {
        
        tutorialsJump.SetActive(true);
        yield return new WaitForSeconds(1);
        tutorialsJump.SetActive(false);
        isTutorialRunning = true;
    }



}