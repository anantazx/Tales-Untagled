using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Unity.VisualScripting;

public class FileDataHandler 
{
    private string dataDirPath = "";

    private string dataFileName = "";
    private bool useEncryption = false;

    private readonly string encryprionWord = "Word";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
        
    }

    public GameData Load(string profileID)
    {
        // base case - jika profileID merupakan null, langsung return
        if (profileID == null)
        {
            return null;
        }

        // menggunakan Path.Combine untuk akun yang memiliki OS berbeda dan pemisah path
        string fullPath = Path.Combine(dataDirPath,profileID, dataFileName);
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

                // optional dalam dekripsi data
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
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


    public void Save(GameData data, string profileID)
    {
        // base case - jika profileID merupakan null, langsung return
        if (profileID == null)
        {
            return;
        }

        // menggunakan Path.Combine untuk akun yang memiliki OS berbeda dan pemisah path
        string fullPath = Path.Combine(dataDirPath,profileID, dataFileName);
        try
        {
            // membuat directory file akan ditulis jika belum ada
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize data C# menjadi JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            //optional enkripsi data yang di save
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

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

    public void Delete(string profileID)
    {
        // if umum jika profilenya null, maka akan return
        if (profileID == null)
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        try
        {
            // memastikan data file ada file ini sebelum mendelete directorynya
            if (File.Exists(fullPath))
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else
            {
                Debug.LogWarning("Tried to delete profile Data, But data was null" + fullPath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to delete profile data for profile ID : " + profileID + "at path : " + fullPath + "\n" + e);
         
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        // menloop semua nama Dictionary di dalam directory path.
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileID = dirInfo.Name;

            // defensice progrramming check apakah data filenya ada
            // jika tidak ada maka folder bukan profile dan bisa di skip
            string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skippig directory when loading, all profiles because it does not cotain data : " +
                    profileID);
                continue;
            }

            // load game data untuk profile ini da menaruh nya dalam dictionary
            GameData profileData = Load(profileID);
            // defensice progrramming memastikan data tidak null
            // jika null maka sesuatu akan salah dan memberi tahu kita sendiri
            if (profileData != null)
            {
                profileDictionary.Add(profileID, profileData);
            }
            else
            {
                Debug.LogWarning("mencoba load profile tapi terjadi suatu kesalahan. ProfileID" + profileID);
            }

        }

        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileID()
    {
        string mostRecentProfileID = null;

        Dictionary<string, GameData> profileGamesData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameData> pair in profileGamesData)
        {
            string profileID = pair.Key;
            GameData gameData = pair.Value;

            // men skip entry ini jika gamedata merupakan null
            if (gameData == null)
            {
                continue;
            }

            // jika ini data pertama yang kita jumpai yang ada maka ini merupaka data palin baru
            if (mostRecentProfileID == null)
            {
                mostRecentProfileID = profileID;
            }
            // kebalikannya, mengcompare untuk melihat data mana yang lebih baru
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profileGamesData[mostRecentProfileID].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);

                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileID = profileID;
                }
            }
        }
        return mostRecentProfileID;
    }

    // dibawah merupakan implementasi gampang dari XOR encryption
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char) (data[i] ^ encryprionWord[i % encryprionWord.Length]);
        }
        return modifiedData;
    }



}
