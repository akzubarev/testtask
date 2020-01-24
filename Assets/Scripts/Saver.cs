using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class Saver : MonoBehaviour
{
    public static GameInformation gamedata;
   static  string dataPath;

    void Awake()
    {
      //  dataPath = Path.Combine(Application.dataPath, "GameData.txt");
        dataPath = Path.Combine(Application.persistentDataPath, "GameData.txt");
    }

    public void Save()
    {
        gamedata = new GameInformation(GameController.levels);
        SaveGame(gamedata, dataPath);
    }

    public static bool Load()
    {
        if (!File.Exists(dataPath))
        { 
            Debug.Log("No File");
            return false;
            }
        else
        {
            gamedata = LoadGame(dataPath);
            GameController.GetFromSave(gamedata.levels);
            return true;
            }
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    static void SaveGame(GameInformation data, string path)
    {
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }

    static GameInformation LoadGame(string path)
    {
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<GameInformation>(jsonString);
        }
    }
}

[System.Serializable]
public class GameInformation
{
    public GameInformation(List<Level> _levels)

    {
        levels=_levels;
    }

    public List<Level> levels;
}

