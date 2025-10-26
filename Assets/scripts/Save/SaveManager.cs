using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string filePath;
    private Save save;

    public Dictionary<int, List<Ghost>> GetGhosts()
    {
        return save.GetGhosts();
    }
    public Inventory GetInventory()
    {
        return save.GetInventory();
    }


    public void Awake()
    {
        filePath = Application.persistentDataPath + "/SaveData.json";
        Debug.Log(Application.persistentDataPath);
    }

    public void SaveToJson() {
        string saveData = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath,saveData);

    }

    public void LoadJson() {
        try
        {
            string saveData = System.IO.File.ReadAllText(filePath);
            save = JsonUtility.FromJson<Save>(saveData);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            save = new Save();
        }
    }

    public void ResetData() {
        save.ResetData();
    }

    public void AddItem(int id, int count) {
        save.AddItem(id,count);
    }
    public void SetGhosts(Dictionary<int, List<Ghost>> g) {
        save.SetGhosts(g);
    }
}
