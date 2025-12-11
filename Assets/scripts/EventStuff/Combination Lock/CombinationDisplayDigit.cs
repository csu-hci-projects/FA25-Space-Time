using UnityEngine;
using System;

public class CombinationDisplayDigit : MonoBehaviour
{
    [SerializeField]
    private int _numDisplayed;
    [SerializeField]
    private Sprite[] sprites;
    public int NumDisplayed {
        get {
            return _numDisplayed;
        }
        set {
            _numDisplayed = value;
            GetComponent<SpriteRenderer>().sprite = sprites[value];
        }
    }
    public void AssignData(int value) {
        NumDisplayed = value;
    }
}
