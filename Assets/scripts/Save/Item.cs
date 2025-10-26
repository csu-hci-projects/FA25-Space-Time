using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
    public int id;
    public string name;
    public string description;
    public int count;

    public Item(int i, string n, string d, int c) {
        id = i;
        name = n;
        description = d;
        count = c;
    }

    public int GetID() {return id;}
    public int GetCount() {return count;}
    public string GetName() {return name;}
    public string GetDescription() {return description;}
    public void ChangeCount(int c) { count += c; Debug.Log("New Count: " + count);}
    public void SetCount(int c) { count = c;}
}
