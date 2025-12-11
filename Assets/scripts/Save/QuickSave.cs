using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuickSave
{
    public int quickRealTime, quickFakeTime;
    public ObjectDataSegment quickObjectData;
    public Vector3 quickPosition;
    public QuickSave(Vector3 pos, ObjectDataSegment seg, int fTime, int rTime)
    {
        quickRealTime = rTime;
        quickFakeTime = fTime;
        quickObjectData = seg;
        quickPosition = pos;
    }
}