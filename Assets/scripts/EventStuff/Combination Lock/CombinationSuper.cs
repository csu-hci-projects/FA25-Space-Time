using UnityEngine;
using System;

public class CombinationSuper : MonoBehaviour, Randomizable
{
    public CombinationReciever combRec;
    public CombinationDisplay combDis;
    public void AssignData(int value) {
        combDis.AssignData(value); 
        combRec.AssignData(value); 
    }
}