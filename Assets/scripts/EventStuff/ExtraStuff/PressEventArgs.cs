using System;
public class PressEventArgs : EventArgs {
    public int SenderID { get; set; }
    public int TargetID { get; set; }

    public PressEventArgs(int s, int t) {
        SenderID = s;
        TargetID = t;
    }
}