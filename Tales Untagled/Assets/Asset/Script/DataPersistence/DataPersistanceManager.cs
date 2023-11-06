    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("Debuging")]
    [SerializeField] private bool initialiezDataIfNull = false;
    [SerializeField] private bool DisableDataPersistence = false;
    [SerializeField] private bool OverRideSelelctedProfileID = false;
    [SerializeField] private string TestSelectedProfileID = "";

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryptionData;

    private GameData gameData;

    private List<IDataPersistence> dataPersistencesObejct;
    private FileDataHandler dataHandler;

    private string selectedProfileID = "";
    public static DataPersistanceManager instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found More Than one Data persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);


        if (DisableDataPersistence)
        {
            Debug.LogWarning("Data Persistence is Currently Disable! ");
        }

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryptionData);

        InitializeSelectedProfileID();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistencesObejct = FindAllDataPersistenceObjects();
        LoadGame();
    }


    public void ChangeSelectedProfileID( string profileID)
    {
        // update profile untuk save dan load
        this.selectedProfileID = profileID;
        // load game, dimana kita akan menggunakan profile id tersebut untuk update game data kita yang sesuai
        LoadGame();
    }

    public void DeleteProfileData(string profileID)
    {
        // mendelete data pada profile yang dipilih
        dataHandler.Delete(profileID);

        //initialisasi profile yang telah dipilih
        InitializeSelectedProfileID();

        // mengulang game agar data sama dengan data yang baru
        LoadGame() ;
    }

    private void InitializeSelectedProfileID()
    {
        this.selectedProfileID = dataHandler.GetMostRecentlyUpdatedProfileID();
        if (OverRideSelelctedProfileID)
        {
            this.selectedProfileID = TestSelectedProfileID;
            Debug.LogWarning("Override selected profile id with test id : " + TestSelectedProfileID);
        }
    }



    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // return langsung jika data persistence disable
        if (DisableDataPersistence)
        {
            return;
        }

        // load save data apa saja dari file menggunakan data handler
        this.gameData = dataHandler.Load(selectedProfileID);

        if (this.gameData == null && initialiezDataIfNull)
        {
            NewGame();
        }

        // jika tidak ada data maka, akan dilanjutkan kepada new game.
        if (this.gameData == null)
        {
            Debug.Log("No save data was found. A New Game needs to be startred");
            return;
        }

        // mendorong data yang di load kepada seluruh script yang membutuhka
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObejct)
        {
            dataPersistenceObj.loadData(gameData);
        }
    }

    public void SaveGame()
    {
        // return langsung jika data persistence disable
        if (DisableDataPersistence)
        {
            return;
        }

        if (this.gameData == null)
        {
            Debug.LogWarning("No Data Was Found. A New Game Needed To Start before data can be saved.");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObejct)
        {
            dataPersistenceObj.saveData(gameData);
        }

        //timestamp data agar kita tahu kapan terakhir di save
        gameData.lastUpdated = System.DateTime.Now.ToBinary();
        
        dataHandler.Save(gameData, selectedProfileID);
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistencesObejct = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistencesObejct);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfileData()
    {
        return dataHandler.LoadAllProfiles();
    }

}
