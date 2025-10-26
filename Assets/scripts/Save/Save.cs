using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public Inventory inventory;
    public Dictionary<int, List<Ghost>> ghosts;
    public Save()
    {
        inventory = new Inventory();
        ghosts = new Dictionary<int, List<Ghost>>();
    }
    public Save(Inventory inv, Dictionary<int, List<Ghost>> gS)
    {
        inventory = inv;
        ghosts = gS;
    }
    public void ResetData()
    {
        inventory.ResetData();
        ghosts = new Dictionary<int, List<Ghost>>();
    }

    public Dictionary<int, List<Ghost>> GetGhosts()
    {
        return ghosts;
    }
    public Inventory GetInventory()
    {
        return inventory;
    }
    public void AddItem(int id, int count)
    {
        inventory.AddItem(id, count);
    }
    public void SetGhosts(Dictionary<int, List<Ghost>> g) {
        ghosts = g;
    }
    public void AddGhost(int i, Ghost g) {
        List<Ghost> gs;
        try {
            gs = ghosts[i];
        }
        catch (Exception e) {
            gs = new List<Ghost>();
            ghosts.Add(i, gs);
        }
        gs.Add(g);
    }
}
