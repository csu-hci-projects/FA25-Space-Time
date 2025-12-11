using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class GhostSegment
{
    public List<Ghost> ghosts;
    public int teleporterID;
    public int time;

    public GhostSegment(int tID, int time)
    {
        teleporterID = tID;
        ghosts = new List<Ghost>();
    }
    public GhostSegment()
    {
        ghosts = new List<Ghost>();
    }

    public List<Ghost> GetGhosts()
    {
        return ghosts;
    }
    public void Add(Ghost g)
    {
        ghosts.Add(g);
    }
    public void AddAll(List<Ghost> gL)
    {
        foreach (Ghost g in gL)
        {
            Add(g);
        }
    }
    public List<Ghost> GetChoppedRemains(int curFrame)
    {
        List<Ghost> outList = new List<Ghost>();
        foreach (Ghost g in ghosts)
        {
            Ghost newGhost = g.GetChopped(curFrame);
            if (newGhost != null)
            {
                outList.Add(newGhost);
            }
        }
        return outList;
    }
    public override string ToString()
    {
        string retString = "{teleporterID: " + teleporterID + "\n";
        foreach (Ghost g in ghosts)
        {
            Debug.Log("Found a ghost!");
            retString += "Ghost: " + JsonUtility.ToJson(g) + "\n";
        }
        return retString + "}";
    }
}