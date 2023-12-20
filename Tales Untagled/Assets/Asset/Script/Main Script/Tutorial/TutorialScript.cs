using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorialsRun;
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
        
        tutorialsRun.SetActive(true);
        yield return new WaitForSeconds(2);
        tutorialsRun.SetActive(false);
        triggerHint.SetActive(false);
        isTutorialRunning = false;
    }



}
