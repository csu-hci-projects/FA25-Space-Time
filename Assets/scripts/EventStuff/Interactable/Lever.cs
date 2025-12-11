using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Lever : Interactable, Savable
{
    private ObjectData objData;
    [SerializeField]
    private InteractionManager iM;
    private int target;
    private bool flipped;
    public override void Reset(int time)
    {
        AssignData(objData);
        
    }
    private void Start()
    {
        iM = GameObject.Find("Manager").GetComponent<InteractionManager>();
        StartUp();
    }
    public void AssignData(ObjectData data)
    {
        if(flipped != data.bool1)
        {
            if(flipped) { OnInteract(0); }
            else { OnCancelInteract(0); }
        }
        flipped = data.bool1;
    }
    
    public void SetObjectData(ObjectData data)
    {
        objData = data;
    }

    public ObjectData GetData()
    {
        return new ObjectData(id, -1, flipped, false, false, transform.parent.position);
    }
    public override void OnInteract(int sender)
    {
        iM.SendStartMessage(id, target);
    }
    
    public override void OnCancelInteract(int sender)
    {
        iM.SendCancelMessage(id, target);
    }
}