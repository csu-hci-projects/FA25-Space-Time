using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrateScript : Interactable, Savable
{
    [SerializeField]
    private ObjectData objData;
    GameObject mainObject;
    private bool isAttached;
    private GameObject attachObject;
    private int attachId;
    private Vector3 prevPosition;
    private Vector3 startPos;

    // Load Savable
    public void AssignData(ObjectData data)
    {
        transform.parent.position = data.position1;
        startPos = data.position1;
    }

    // Write savable
    public ObjectData GetData()
    {
        objData = new ObjectData(id, -1, false, false, false, transform.parent.position);
        return objData;
    }

    // Start Interactable
    public override void OnInteract(int senderId)
    {
        GetAttached();
        isAttached = true;
        prevPosition = attachObject.transform.position;
        attachId = senderId;
    }
    public override void OnCancelInteract(int senderId)
    {
        if (senderId == attachId)
        {
            isAttached = false;
            attachId = int.MinValue;
        }
    }
    
    public void SetObjectData(ObjectData data)
    {
        objData = data;
    }

    // Start TravelEffected
    public override void Reset(int time)
    {
        AssignData(objData);
        attachId = int.MinValue;
        isAttached = false;
        attachObject = null;
    }

    private void GetAttached()
    {
        attachObject = currentTouching.Values.Last();
    }

    void Update()
    {
        if (isAttached)
        {
            mainObject.transform.position += attachObject.transform.position - prevPosition;
            prevPosition = attachObject.transform.position;
        }
    }
    
    void Start()
    {
        mainObject = transform.parent.gameObject;
        StartUp();
        isAttached = false;
        attachObject = player;
        startPos = transform.position;
        attachId = int.MinValue;
    }
}
