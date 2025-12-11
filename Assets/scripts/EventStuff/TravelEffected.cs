using UnityEngine;
using System;

public abstract class TravelEffected : MonoBehaviour
{
    public int id;
    public int GetID(){return id;}
    public abstract void Reset(int time);
}