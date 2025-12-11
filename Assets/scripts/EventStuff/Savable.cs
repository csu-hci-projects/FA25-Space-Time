using UnityEngine;
using System;

public interface Savable
{
    public abstract ObjectData GetData();
    public abstract void AssignData(ObjectData data);
    public abstract void SetObjectData(ObjectData data);
    public int GetID();
}