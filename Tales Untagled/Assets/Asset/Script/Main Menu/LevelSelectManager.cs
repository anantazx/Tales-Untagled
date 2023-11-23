using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectManager : Menu
{
    public int currentLevel {  get; private set; }
    public static LevelSelectManager instance { get; private set; }

    [SerializeField] private GameObject popUp;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one LevelSelectManager in the scene");
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        
        GetprofileID();
    }

  


    public void LevelCompleted(int currentLevel)
    {
        Debug.Log("Current level" + currentLevel);

        if (DataPersistanceManager.instance.IsLevelUnlocked(currentLevel))
        {
            int starCounter = DataPersistanceManager.instance.GetStarCountForLevels(currentLevel);
            Debug.Log("starCounter " + starCounter);

            DataPersistanceManager.instance.CollectStar(currentLevel, starCounter);

            // membuka next level
            int nextLevel = currentLevel + 1;
            DataPersistanceManager.instance.UnlockedLevel(nextLevel);

            //save game data
            DataPersistanceManager.instance.SaveGame();

        }
        else
        {
            Debug.LogWarning("Level is not unlocked: " + currentLevel);
        }
    }

    public void SelectLevel(int levelNumber)
    {
        string levelSceneName = "Level " + levelNumber;
        SceneManager.LoadSceneAsync(levelSceneName);

        SetCurrentLevel(levelNumber);
        GetprofileID();
    }
    
    private void GetprofileID()
    {
        string selectedProfileID = DataPersistanceManager.instance.GetSelectedProfileID();
        if (string.IsNullOrEmpty(selectedProfileID))
        {
            selectedProfileID = "Test";
        }
        DataPersistanceManager.instance.ChangeSelectedProfileID(selectedProfileID);
        Debug.Log("profileID : " + selectedProfileID);
    }

    private void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }

    public void BackToMenu()
    {
        DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void ActivePopUp()
    {
        StartCoroutine(PopUp());
    }
    
    private IEnumerator PopUp()
    {
        popUp.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        popUp.gameObject.SetActive(false);
    }
    
}
