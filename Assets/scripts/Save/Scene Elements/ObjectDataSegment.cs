using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
[Serializable]
public class ObjectDataSegment
{
    public List<ObjectData> objectData;
    public int teleporterID;
    public int time;
    public ObjectDataSegment(int teleporterID, List<ObjectData> objectData) // For set up and quick save.
    {
        this.teleporterID = teleporterID;
        this.objectData = objectData;
        time = 0;
    }
    public ObjectDataSegment(int tID, int time)
    {
        teleporterID = tID;
        objectData = new List<ObjectData>();
        this.time = time;
    }
    public ObjectDataSegment()
    {
        objectData = new List<ObjectData>();
    }

    public List<ObjectData> getGhosts()
    {
        return objectData;
    }
    public void Add(ObjectData o)
    {
        objectData.Add(o);
    }
    public void AddAll(List<ObjectData> oL)
    {
        foreach (ObjectData o in oL)
        {
            Add(o);
        }
    }
    public void Clear()
    {
        objectData.Clear();
    }
    public ObjectData Load(int id)
    {
        return objectData.Find(x => x.int1 == id);
    }
}