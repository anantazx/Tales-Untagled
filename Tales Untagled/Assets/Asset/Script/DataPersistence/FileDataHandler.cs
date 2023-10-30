using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler 
{
    private string dataDirPath = "";

    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        // menggunakan Path.Combine untuk akun yang memiliki OS berbeda dan pemisah path
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load data serialized dari file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // deserialized data dari Json balik menjadi C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }


    public void Save(GameData data)
    {
        // menggunakan Path.Combine untuk akun yang memiliki OS berbeda dan pemisah path
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // membuat directory file akan ditulis jika belum ada
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize data C# menjadi JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            // menulis serialize data pada file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when tryig to save data to file: " + fullPath + "\n" + e);
            
        }
    }
}
