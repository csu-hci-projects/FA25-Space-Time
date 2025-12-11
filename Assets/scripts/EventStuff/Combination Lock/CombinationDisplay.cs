using UnityEngine;
using System;
public class CombinationDisplay : MonoBehaviour
{
    [SerializeField]
    private int expectedCombination;
    [SerializeField]
    private GameObject[] digitObjects;
    private CombinationDisplayDigit[] digits;
    private int[] nums;
    private int numDigits;

    public void AssignData(int value) {
        for(int i = numDigits-1; i >= 0; i--) {
            digits[i].AssignData(value%10);
            value = (int)(value/10);
        }
    }
    void Start() {
        numDigits = digitObjects.Length;
        digits = new CombinationDisplayDigit[numDigits];
        for(int i = 0; i < numDigits; i++) {
            digits[i] = digitObjects[i].GetComponent<CombinationDisplayDigit>();
        }
    }
}