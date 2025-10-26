using System;
using UnityEngine;

[System.Serializable]
public class ElevatorSetUpArgs : PESetUpArgs
{
    public int ID;
    public Vector3 startPos;
    public float height;
    public float speed;
    public ElevatorSetUpArgs(int i, Vector3 start, float h, float s) : base(i)
    {
        startPos = start;
        height = h;
        speed = s;
    }
}