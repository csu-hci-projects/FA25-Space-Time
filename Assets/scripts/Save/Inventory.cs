using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory {
    private int money;
    public bool maxInventory;
    public List<Item> items;

    public Inventory() {
        money = 0;
        maxInventory = false;
        items = new List<Item>();
        // CreateItem(new Item(0,"Iron Bar","Useful for some upgrades",0));
        // CreateItem(new Item(1,"Gold Bar","Useful for some upgrades",0));
        // CreateItem(new Item(2,"Azur Bar","Useful for some upgrades",0));
        // CreateItem(new Item(3,"Bloody Bar","Useful for some upgrades",0));
    }
    public void ResetData()
    {
        maxInventory = false;
        SetMoney(0);
        RemoveAllItems();
    }
    public void ChangeMoney(int v) {money += v;}
    public void SetMoney(int v) {money += v;}
    public int GetMoney() {return money;}
    public List<Item> GetItemList() { return items; }
    public void CreateItem(Item item) {items.Add(item);}
    public void AddItem(int id, int c) {items[id].ChangeCount(c);}
    public Item GetItem(int id){return items[id];}
    public void RemoveAllItems() {
        foreach (Item i in items) {
            i.SetCount(0);
        }
    }
}
