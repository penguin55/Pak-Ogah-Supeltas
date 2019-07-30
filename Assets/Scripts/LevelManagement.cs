using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public static List<Level> levels = new List<Level>();
    public static int levelCurrent;
    public static LevelManagement levelManager;

    void Start()
    {
        levelManager = this;
        Load();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file =
            File.Create(Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, levels);
        file.Close();
    }

    public void Load()
    {
        levels.Clear();
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            levels = (List<Level>) bf.Deserialize(file);
            file.Close();
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                levels.Add(new Level("Level "+(i+1)));
            }
            Save();
        }

    }
}

[Serializable]
public class Level
{
    public string levelName;
    public int stars;

    public Level (string _levelName)
    {
        levelName = _levelName;
        stars = 0;
    }
}
