using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;

[System.Serializable]
public class MyItemInfo
{
    public int id;  // 아이템 ID.
    public int invenIndex;  // 인벤토리 위치.
}

[System.Serializable]
public class GameData
{
    public int gold = 0;    // 재화.
    public int cleardStage = 0; // 클리어 한 스테이지.
    public List<MyItemInfo> items = new List<MyItemInfo>(); // 내가 보유한 아이템 정보 리스트.
}

public static class DataManager
{
    public static GameData currentData = new GameData();
    private static string path = Path.Combine(Application.persistentDataPath, "saveData.json");

    public static void Save()
    {
        string json = JsonUtility.ToJson(currentData);
        File.WriteAllText(path, json);
    }

    public static void Load()
    {
        if(File.Exists(path) == true)
        {
            string json = File.ReadAllText(path);
            currentData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            currentData.gold = 100;
            currentData.cleardStage = 0;
            currentData.items.Clear();
        }
    }

    public static void AddItem(MyItemInfo item)
    {
        currentData.items.Add(item);
        Save();
    }

    public static void SetGold(int gold)
    {
        currentData.gold = gold;
        Save();
    }

    public static void SetCleardStage(int stage)
    {
        currentData.cleardStage = stage;
        Save();
    }

    public static int GetGold()
    {
        return currentData.gold;
    }

    public static int GetClearedStage()
    {
        return currentData.cleardStage;
    }
}
