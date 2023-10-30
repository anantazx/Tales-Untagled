    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private GameData gameData;

    private List<IDataPersistence> dataPersistencesObejct;
    private FileDataHandler dataHandler;
    public static DataPersistanceManager instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found More Than one Data persistence Manager in the scene");
        }
        instance = this;

    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistencesObejct = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // load save data apa saja dari file menggunakan data handler
        this.gameData = dataHandler.Load();

        // jika tidak ada data maka, akan dilanjutkan kepada new game.
        if (this.gameData == null)
        {
            Debug.Log("No save data was found");
            NewGame();
        }

        // mendorong data yang di load kepada seluruh script yang membutuhka
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObejct)
        {
            dataPersistenceObj.loadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObejct)
        {
            dataPersistenceObj.saveData(ref gameData);
        }

        
        dataHandler.Save(gameData);
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistencesObejct = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistencesObejct);
    }

}
