using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Position is decided starting in order of wall from left, bottom, right, top. It is always left to right or top to bottom.
public class Room {
    private List<Vector3> enterCoords;
    private GameObject tileMapParent;
    private int[,] exitRooms;
    private string name;
    private int numExits, mainArrPos;

    public Room(int num, string n, GameObject tmParent, List<Vector3> enPos) {
        mainArrPos = num;
        name = n;
        enterCoords = enPos;
        numExits = enterCoords.Count;
        exitRooms = new int[numExits,2];
    }

    public void AddExit(int rNum, int pos, int eNum) { // pos is the exit number of the current room. rNum is the room number that the exit leads to. eNum is the entrance number to define player pos in next room.
        exitRooms[pos,0] = rNum;
        exitRooms[pos,1] = eNum;
    }

    public int GetNextEntrance(int exitNum) {
        return exitRooms[exitNum,1];
    }

    public int GetExit(Room room) { 
        int rNum = room.getRoomNum();
        for(int i = 0; i < numExits; i++) {
            if(rNum == exitRooms[i,0]) {
                return i;
            }
        }
        return -1;
    }

    public int GetNextRoom(int exitNum) {
        return exitRooms[exitNum,0];
    }

    public Vector3 GetEnterCoords(int i) {
        return enterCoords[i];
    }

    public int getRoomNum() {
        return mainArrPos;
    }

    public void SetActive(bool b) {
        tileMapParent.SetActive(b);
    }

    public void PrintExitData() {
        int l0 = exitRooms.GetLength(0);
        int l1 = exitRooms.GetLength(1);
        string temp;
        for(int i = 0; i < l0; i++) {
            temp = mainArrPos + ": ";
            for(int j = 0; j < l1; j++) {
                temp += exitRooms[i,j] + " ";
            }
            Debug.Log(temp);
        }
    }
} 
