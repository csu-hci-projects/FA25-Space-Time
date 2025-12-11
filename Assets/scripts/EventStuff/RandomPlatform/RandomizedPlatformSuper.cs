using UnityEngine;
using System;

public class RandomizedPlatformSuper : MonoBehaviour, Randomizable
{
    [SerializeField]
    private RandomizedPlatform[] randomPlatforms;
    public void AssignData(int value) {
        foreach(RandomizedPlatform rP in randomPlatforms) {rP.AssignData(value); }
    }
    private void Start()
    {
        int total = randomPlatforms.Length;
        for(int i = 0; i < total; i++) {
            randomPlatforms[i].SetStartCond(i, total);
        }
    }
}