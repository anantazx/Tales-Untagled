using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotManager : Menu
{
    private SaveSlot[] saveSlots;
    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject saveSlotUI;
    [SerializeField] private GameObject deleteSaveSlotUI;
    [SerializeField] private Slider loadingSlider;

    private bool IsLoadingGame = false;

    [Header("MenuButton")]
    [SerializeField] private Button backButton;

    [Header("PopUps")]
    [SerializeField] private PopUp popUp;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        // menonaktifkan semua tomboll
        DisableMenuButtons();

        // kasus - load Game
        if (IsLoadingGame)
        {
            DataPersistanceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());
            SaveGameAndLoadGame();
        }
        //kasus - new Game, namun save slot ada data savenya
        else if (saveSlot.hasData)
        {
            popUp.ActivatedMenu(
                "Are you sure you want to start a new game and overwrite this save data ? ",
                // function untuk mengeksekusi jika kita menekan yes
                () =>
                {
                    DataPersistanceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());
                    DataPersistanceManager.instance.NewGame();
                    SaveGameAndLoadGame();
                },
                // function untuk mengeksekusi jika kita menekan tidak
                () =>
                {
                    this.ActivationMenu(IsLoadingGame);
                }
            );
            

        }
        // case - new game, dan save slot tidak ada data
        else
        {
            DataPersistanceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());
            DataPersistanceManager.instance.NewGame();
            SaveGameAndLoadGame();
        }
    }

    private void SaveGameAndLoadGame()
    {
        DataPersistanceManager.instance.SaveGame();

        // load scene dimana aka mengaktifkan save games karena OnSceneUnload() pada DataPersistanceManager

        mainMenuUI.SetActive(false);
        loadingScreen.SetActive(true);

        Debug.Log("load Game");
        StartCoroutine(LoadLevelAsync("Chapter Select"));

    }

    public void ActivationMenu(bool isLoadingGame)
    {

        this.IsLoadingGame = isLoadingGame;

        // load semua profile data yang ada
        Dictionary<string, GameData> profileGamesData = DataPersistanceManager.instance.GetAllProfileData();

        backButton.interactable = true;

        // menloop melaluai setiap save masing masing slot yang ada pada ui dan menset conten secara benar
        GameObject firstSelected = backButton.gameObject;
        foreach (SaveSlot saveSlots in saveSlots)
        {
            GameData profileData = null;
            profileGamesData.TryGetValue(saveSlots.GetProfileID(), out profileData);
            saveSlots.SetData(profileData);

            if (profileData == null && isLoadingGame)
            {
                saveSlots.SetInteractabel(false);
            }
            else
            {
                saveSlots.SetInteractabel(true);
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlots.gameObject;
                }
            }   

        }

        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        this.SetFirstSelected(firstSelectedButton);
    }

    public void OnDeleteClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();

        popUp.ActivatedMenu(
            "Are you sure you want to delete this save data ?, all progress will be lost",
            () =>
            {
                DataPersistanceManager.instance.DeleteProfileData(saveSlot.GetProfileID());
                ActivationMenu(IsLoadingGame);
            },
            () =>
            {
                ActivationMenu(IsLoadingGame);
            }
            
        );
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractabel(false);
        }
        backButton.interactable = false;
    }


    private IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }

}
