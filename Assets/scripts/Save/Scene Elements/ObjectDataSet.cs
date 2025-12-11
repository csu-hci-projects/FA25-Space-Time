using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectDataSet
{
    public List<ObjectDataSegment> objectData;
    public int Count;

    public ObjectDataSet()
    {
        objectData = new List<ObjectDataSegment>();
        Count = 0;
    }

    public void SetCheckPoint(int teleporterID, int time)
    {
        ObjectDataSegment newSegment = new ObjectDataSegment(teleporterID, time);
        Push(newSegment);
    }

    public ObjectData Load(int id)
    {
        return Peek().Load(id);
    }

    public void PushAll(List<ObjectDataSegment> objectData)
    {
        this.objectData = objectData;
        Count = objectData.Count;
    }

    public void Clear() { Pop(); SetCheckPoint(0,0); }
    public void ClearAll() { objectData.Clear(); }
    public void Add(ObjectData o) { Peek().Add(o); }
    public void AddAll(List<ObjectData> oL) { Peek().AddAll(oL); }
    public ObjectDataSegment Peek() { return objectData[objectData.Count - 1]; }
    public ObjectDataSegment Pop()
    {
        ObjectDataSegment oS = objectData[objectData.Count - 1];
        objectData.RemoveAt(objectData.Count - 1);
        Count--;
        return oS;
    }
    public void Push(ObjectDataSegment oS)
    {
        Count++;
        objectData.Add(oS);
    }

    
}