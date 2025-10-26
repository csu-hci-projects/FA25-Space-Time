using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Ghost
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int id;
    public Color color;
    public Vector3 curPosition;
    public List<Vector3> positions;
    public List<bool> interactions;
    public int startFrame;
    public int endFrame;
    public int numFrames;

    // Returns a new Ghost Object based on what is left after chopping the current one.
    public Ghost GetChopped(int frame)
    {
        int startIndex = frame - startFrame;
        Debug.Log("Frame: " + frame + " End: " + endFrame);
        if (endFrame <= frame)
        {
            return null;
        }
        List<Vector3> newPositions = new List<Vector3>();
        List<bool> newInteractions = new List<bool>();
        for (int i = startIndex; i < numFrames - 1; i++)
        {
            newPositions.Add(positions[i]);
            newInteractions.Add(interactions[i]);
        }
        return new Ghost(color, newPositions, newInteractions, frame, id);
    }

    public Ghost(Color c, List<Vector3> p, List<bool> i, int s, int ID)
    {
        id = ID;
        positions = new List<Vector3>();
        interactions = new List<bool>();
        foreach (Vector3 v in p)
        {
            if (v != null)
            {
                positions.Add(v);
            }
            else
            {
                Debug.Log("Fake Vector");
            }
        }
        foreach(bool b in i)
        {
            interactions.Add(b);
        }
        color = c;
        startFrame = s;
        numFrames = p.Count;
        endFrame = startFrame + numFrames;
        // Debug.Log("NumFrames Recieved:" + p.Count);
    }

    public void StartGhost(GhostController gC, TimeManager tM)
    {
        // Debug.Log("NumFrames Sent:" + positions.Count);
        gC.OnStartGhost(color, positions, interactions, startFrame, tM, id);
    }


}