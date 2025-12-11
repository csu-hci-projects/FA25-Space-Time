using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
/*
Goal: Create a Serializable list of relevant savestates to recreate any given state from a JSON.
Current Implementation is suboptimal.
ID Format: A BBBB CC
A = instance(arbitrary) (1 -> 9). Mostly to avoid starting 0s. Will need to change to (10 -> 99) if I for some reason have more than 9 of a single object type in a single room.
BBBB = Area (00 00 -> 99 99) Chapter-Room Format 
CC = Type
- 01: Player
- 02: Ghost
- 03: Teleporter
- 04: Crate
- 05: Button
- 06: Door
- 07: Elevator
- 08: Floating Platform
- 09: Lever
- 10: Timed Lever
- 11: Combination Reciever
- 12: Combination Digit 
*/
[Serializable]
public struct ObjectData
{
    public int id;
    public int int1;
    public int int2;
    public bool bool1;
    public bool bool2;
    public bool bool3;
    public Vector3 position1;
    public ObjectData(int i, int i1, bool b1, bool b2, bool b3, Vector3 v1)
    {
        id = i;
        int1 = i1;
        int2 = 0;
        bool1 = b1;
        bool2 = b2;
        bool3 = b3;
        position1 = v1;
    }
    public ObjectData(int i, int i1, int i2, Vector3 v1)
    {
        id = i;
        int1 = i1;
        int2 = i2;
        bool1 = false;
        bool2 = false;
        bool3 = false;
        position1 = v1;
    }
    public ObjectData(int i, int i1, int i2) {
        id = i;
        int1 = i1;
        int2 = i2;
        bool1 = false;
        bool2 = false;
        bool3 = false;
        position1 = new Vector3();
    }
}