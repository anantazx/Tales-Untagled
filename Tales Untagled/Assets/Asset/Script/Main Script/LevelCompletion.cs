using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletion : MonoBehaviour
{

    [SerializeField] GameObject Triggerhint;
    private bool isInRange = false;

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerHasCollectedStars())
            {
                LevelSelectManager.instance.LevelCompleted(LevelSelectManager.instance.currentLevel);
                DataPersistanceManager.instance.SaveGame();
                SceneManager.LoadSceneAsync("Level Selection");
                Debug.Log("level Complete");
            }
            else
            {
                Debug.LogWarning("Player Has not collected the stars");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Triggerhint.SetActive(true);
            isInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Triggerhint.SetActive(false);
            isInRange = false;
        }
    }

    private bool PlayerHasCollectedStars()
    {
        int starCounter = DataPersistanceManager.instance.GetStarCountForLevels(LevelSelectManager.instance.currentLevel);
        Debug.Log($"at LevelCompleteFunction(). Star Counter:  {starCounter}, ItemCollector Stars: {ItemCollector.Instance.stars}, Finish level:  {LevelSelectManager.instance.currentLevel}");

        DataPersistanceManager.instance.SaveGame();

        return starCounter > 0;
    }
}
