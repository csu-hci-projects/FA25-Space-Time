using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    GameObject player;
    List<Savable> savables;
    private string filePath;
    [SerializeField]
    private Save save;

    public List<Ghost> GetGhosts()
    {
        return save.GetGhosts();
    }

    public Inventory GetInventory()
    {
        return save.GetInventory();
    }
    public ObjectData Load(int id)
    {
        return save.Load(id);
    }


    public void Awake()
    {
        filePath = Application.persistentDataPath + "/SaveData.json";
        savables = new List<Savable>();
        player = GameObject.Find("Player");
        // Note: Could probably replace this costly search with some update logic in savables that causes them to push their data through.
        FindObjectsByType<TravelEffected>(FindObjectsSortMode.None).ToList().FindAll(t => t is Savable).ForEach(t => savables.Add(t as Savable));
    }
    

    public QuickSave QuickLoad() // Loads most recent snapshot, and most recent ghost segment, if there is one.
    {
        LoadJSON();
        // Load ObjectData from quicksave
        QuickLoadObjectData();
        LoadObjectData();
        return save.quickSave;
    }

    public void QuickLoadObjectData()
    {
        foreach (Savable s in savables)
        {
            s.AssignData(save.quickSave.quickObjectData.Load(s.GetID()));
        }
    }

    public void LoadObjectData()
    {
        foreach (Savable s in savables)
        {
            Debug.Log(save.objectData.objectData.Count);
            s.SetObjectData(save.objectData.Peek().Load(s.GetID()));
        }
    }


    public void LoadJSON() {
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

    public void UpdateData(int teleporterID, int time)
    {
        save.SetCheckPoint(teleporterID, time);
        List<ObjectData> data = new List<ObjectData>();
        foreach (Savable s in savables)
        {
            data.Add(s.GetData());
        }
        save.UpdateValues(teleporterID, data, time);
        SaveToJSON();
    }

    public void SaveToJSON() {
        string saveData = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath, saveData);
    }

    public void QuickSave(Vector3 pos, int fTime, int rTime) // Saves all object and player data as a snapshot, and 
    {
        List<ObjectData> data = new List<ObjectData>();
        foreach (Savable s in savables)
        {
            data.Add(s.GetData());
        }
        save.quickSave = new QuickSave(pos, new ObjectDataSegment(-1, data), fTime, rTime);
        SaveToJSON();
    }
    
    public void ResetData() {
        save.ResetData();
    }

    public void AddItem(int id, int count) {
        save.AddItem(id,count);
    }
    public void SetObjectData(ObjectDataSet objD)
    {
        save.SetObjectData(objD);
    }
    public void SetGhosts(GhostData g)
    {
        save.SetGhosts(g);
    }
    public void AddGhost(Ghost g)
    {
        save.AddGhost(g);
    }
    public void ClearGhosts()
    {
        save.ClearGhosts();
    }
    public void PrintData()
    {
        Debug.Log("Data should be stored at: " + Application.persistentDataPath + "/SaveData.json");
        Debug.Log(save.ToString());
    }
}
