using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Saves most recent room entrance position
// Saves upgrades
// Saves 
[System.Serializable]
public class Save
{
    public Inventory inventory;
    public ObjectDataSet objectData;
    public GhostData ghosts;
    public bool canTimeTravel;
    public bool canMakeGhosts;
    public int time;
    public int teleporterID;
    public int upgradeAbility;
    public QuickSave quickSave;

    public Save()
    {
        inventory = new Inventory();
        ghosts = new GhostData();
        objectData = new ObjectDataSet();
        time = 0;
        teleporterID = 0;
        canTimeTravel = false;
        canMakeGhosts = false;
    }

    public Save(GhostData g, Inventory inv)
    {
        inventory = inv;
        ghosts = g;
        objectData = new ObjectDataSet();
        time = 0;
        teleporterID = 0;
        canTimeTravel = false;
        canMakeGhosts = false;
    }

    public ObjectData Load(int id)
    {
        return objectData.Load(id);
    }

    public Save(GhostData g, ObjectDataSet obj)
    {
        objectData = obj;
        inventory = new Inventory();
        ghosts = g;
        time = 0;
        teleporterID = 0;
    }

    public void UpdateValues(int teleporterID, List<ObjectData> data, int time)
    {
        objectData.SetCheckPoint(teleporterID, time);
        objectData.AddAll(data);
        ghosts.SetCheckPoint(teleporterID, time);
        this.teleporterID = teleporterID;
        this.time = time;
    }

    public void ResetData()
    {
        inventory.ResetData();
        ghosts = new GhostData();
        objectData = new ObjectDataSet();
        canTimeTravel = false;
        canMakeGhosts = false;
    }

    public List<Ghost> GetGhosts()
    {
        return ghosts.GetGhosts();
    }

    public ObjectDataSet getObjectDataSet()
    {
        return objectData;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public void AddItem(int id, int count)
    {
        inventory.AddItem(id, count);
    }

    public void SetGhosts(GhostData gD)
    {
        ghosts = gD;
    }

    public void ClearGhosts()
    {
        ghosts.Clear();
    }

    public void SetObjectData(ObjectDataSet obj)
    {
        objectData = obj;
    }

    public void AddGhost(Ghost g)
    {
        ghosts.Add(g);
    }

    public void SetCheckPoint(int teleporterID, int time)
    {
        ghosts.SetCheckPoint(teleporterID, time);
    }

    public override string ToString()
    {
        // retString = "";
        string g = ghosts.SegToString();
        return g;
    }
}
