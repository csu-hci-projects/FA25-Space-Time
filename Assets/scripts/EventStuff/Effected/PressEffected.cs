using System;
using UnityEngine;

public abstract class PressEffected<TArgs> : TravelEffected where TArgs : PESetUpArgs
{
    private InteractionManager iM;
    protected int id;
    protected void GetIM() {
        iM = GameObject.Find("Manager").GetComponent<InteractionManager>();
        Subscribe(iM);
    }
    private void Subscribe(InteractionManager i)
    {
        i.PressStartEvent += OnStartCheck;
        i.PressCancelEvent += OnCancelCheck;
        Debug.Log("Subscribing!!!");
    }
    protected abstract void SetUp(TArgs args);
    protected void OnStartCheck(object sender, PressEventArgs e)
    {
        if(e.TargetID == id)
        {
            OnStart(e.SenderID);
        }
    }
    protected void OnCancelCheck(object sender, PressEventArgs e)
    {
        if(e.TargetID == id)
        {
            OnCancel(e.SenderID);
        }
    }
    protected abstract void OnStart(int senderID);
    protected abstract void OnCancel(int senderID);
}