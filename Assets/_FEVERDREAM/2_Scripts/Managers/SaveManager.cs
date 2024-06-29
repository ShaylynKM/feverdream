using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
public class SaveManager
{
    // Using a binary formatter to create a secure save file

    private static string saveGamePath = Application.persistentDataPath + "/savedata.data"; // Creating a file path for the saved data

    public static void OnSave(string savedScene)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(saveGamePath, FileMode.Create);
        SaveData data = new SaveData(savedScene);

        formatter.Serialize(stream, data); // Serialize SaveData object
        stream.Close(); // Close filestream
    }
    public static SaveData OnLoadGame()
    {
        if(File.Exists(saveGamePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(saveGamePath, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close(); // close filestream

            return data;
        }
        else
        {
            return null;
        }
    }

    // Used for testing purposes
    public static void ClearSave()
    {
        if(File.Exists(saveGamePath))
        {
            File.Delete(saveGamePath); // Deletes the data
            Debug.Log("Cleared save data");
        }
        else
        {
            Debug.Log("No saved data to clear");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string SavedScene;

    public SaveData(string savedScene)
    {
        SavedScene = savedScene;
    }
}
