using UnityEngine;
using System;
public class InteractionManager : MonoBehaviour
{
    public event EventHandler<PressEventArgs> PressStartEvent, PressCancelEvent;

    public void SendStartMessage(int sender, int target) {
        // Debug.Log("Started!!!");
        PressEventArgs args = new PressEventArgs(sender, target);
        PressStartEvent?.Invoke(this, args);
    }
    public void SendCancelMessage(int sender, int target) {
        // Debug.Log("Cancelled!!!");
        PressEventArgs args = new PressEventArgs(sender, target);
        PressCancelEvent?.Invoke(this, args);
    }

}
