using System;
using System.Collections.Generic;
using System.Linq;

// Ultimately decided to create a new class to handle the fact that I'm storing data in the form Stack<Dictionary<int, List<Ghost>>>
public class GhostData
{
    private Stack<GhostSegment> ghosts;
    public GhostData()
    {
        ghosts = new Stack<GhostSegment>();
    }
    public List<Ghost> getGhosts()
    {
        return ghosts.Peek().getGhosts();
    }
    public void SetCheckPoint(int time) {
        GhostSegment newSegment = new GhostSegment();
        if(ghosts.Count > 0)
        {
            // Debug.Log("Doing a thing");
            GhostSegment g = ghosts.Peek();
            List<Ghost> gL = g.GetChoppedRemains(time);
            newSegment.AddAll(gL);
        }
        ghosts.Push(newSegment); 
    }
    public void Clear(){ ghosts.Pop(); SetCheckPoint(0); }
    public void ClearAll(){ ghosts.Clear(); }
    public void Add(Ghost g){ ghosts.Peek().Add(g); }

    public class GhostSegment
    {
        private List<Ghost> ghosts;
        private int teleporterID;

        public GhostSegment(int tID)
        {
            teleporterID = tID;
            ghosts = new List<Ghost>();
        }
        public GhostSegment()
        {
            ghosts = new List<Ghost>();
        }

        public List<Ghost> getGhosts()
        {
            return ghosts;
        }
        public void Add(Ghost g)
        {
            ghosts.Add(g);
        }
        public void AddAll(List<Ghost> gL)
        {
            foreach(Ghost g in gL)
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
    }
}