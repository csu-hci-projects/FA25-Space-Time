using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Ultimately decided to create a new class to handle the fact that I'm storing data in the form Stack<Dictionary<int, List<Ghost>>>
[System.Serializable]
public class GhostData
{
    public List<GhostSegment> ghosts;
    public int Count;

    public GhostData()
    {
        ghosts = new List<GhostSegment>();
        Count = 0;
    }
    public List<Ghost> GetGhosts()
    {
        return Peek().GetGhosts();
    }
    public void SetCheckPoint(int teleporterID, int time)
    {
        GhostSegment newSegment = new GhostSegment(teleporterID, time);
        if (Count > 0)
        {
            GhostSegment g = Peek();
            List<Ghost> gL = g.GetChoppedRemains(time);
            newSegment.AddAll(gL);
        }
        Push(newSegment);
    }
    public void Clear() { Pop(); SetCheckPoint(0,0); }
    public void ClearAll() { ghosts.Clear(); }
    public void Add(Ghost g) { Peek().Add(g); }
    public GhostSegment Peek() { return ghosts[ghosts.Count - 1]; }
    public GhostSegment Pop()
    {
        GhostSegment gS = ghosts[ghosts.Count - 1];
        ghosts.RemoveAt(ghosts.Count - 1);
        Count--;
        return gS;
    }
    public void Push(GhostSegment gS)
    {
        Count++;
        ghosts.Add(gS);
    }
    public string SegToString()
    {
        if (ghosts.Count > 1)
        {
            return JsonUtility.ToJson(Peek());
        }
        else
        {
            return "";
        }
    }
    
}