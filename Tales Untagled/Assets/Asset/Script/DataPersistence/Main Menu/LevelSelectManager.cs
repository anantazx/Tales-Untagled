using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : Menu
{
    public int currentLevel {  get; private set; }
    public static LevelSelectManager instance { get; private set; }

    [SerializeField] private GameObject popUp;

    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject starLevel;
    [SerializeField] private Slider loadingSlider;

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
       

        if (DataPersistanceManager.instance.IsLevelUnlocked(currentLevel))
        {
            int starCounter = DataPersistanceManager.instance.GetStarCountForLevels(currentLevel);
            

            DataPersistanceManager.instance.CollectStar(currentLevel, starCounter);

            // membuka next level
            int nextLevel = currentLevel + 1;
            DataPersistanceManager.instance.UnlockedLevel(nextLevel);
            Debug.Log("Next level" + nextLevel);
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
        levelSelect.SetActive(false);
        starLevel.SetActive(false);
        loadingScreen.SetActive(true);

        string levelSceneName = "Level " + levelNumber;
        StartCoroutine(LoadLevelAsync(levelSceneName));


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
        SceneManager.LoadSceneAsync("Chapter Select");
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

    private IEnumerator LoadLevelAsync(string sceneManager)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneManager);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
    
}
