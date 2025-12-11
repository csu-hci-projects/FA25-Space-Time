using UnityEngine;
using System;

public class CombinationReciever : Interactable, Savable
{
    private InteractionManager interactionManager;
    [SerializeField]
    private ObjectData objData;
    [SerializeField]
    private int target;
    private int combination;
    [SerializeField]
    private int expectedCombination;
    [SerializeField]
    private GameObject[] digitObjects;
    private CombinationDigit[] digits;
    private int[] nums;
    private int numDigits;

    protected void OnStart(int senderID) {
        int instanceNumber = (int) Math.Floor(senderID / 100000f);
    }

    public override void OnInteract(int sender) {
        CheckCombination();
        Debug.Log("Expected: " + expectedCombination + ". Actual: " + combination);
        if(combination == expectedCombination) {
            interactionManager.SendStartMessage(id, target);
        }
        else {
            interactionManager.SendCancelMessage(id, target);
        }
    }

    void Start()
    {
        StartUp();
        interactionManager = GameObject.Find("Manager").GetComponent<InteractionManager>();
        numDigits = digitObjects.Length;
        digits = new CombinationDigit[numDigits];
        nums = new int[numDigits];
        for(int i = 0; i < numDigits; i++) {
            nums[i] = 0;
            digits[i] = digitObjects[i].GetComponent<CombinationDigit>();
        }
    }

    public ObjectData GetData() {
        return objData;
    }
    public void AssignData(ObjectData data) {
        expectedCombination = data.int1;
        combination = data.int2;
    }
    public void SetObjectData(ObjectData data) {
        objData = data;
    }
    public override void Reset(int time) {
        AssignData(objData);
    }
    private void CheckCombination() {
        combination = 0;
        for(int i = 0; i < numDigits; i++) {
            combination += digits[i].GetNum() * (int)Math.Pow(10, numDigits-1-i);
        }
    }
    public void AssignData(int value) {
        expectedCombination = value;
    }
    // What if a single combo digit linked to multiple recievers?
    // What about the overhead of checking every digit every frame? Should be negligable relative to everything else tbh.
}
