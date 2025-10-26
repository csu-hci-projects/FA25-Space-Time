using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGraph
{
    private List<Room> allRooms;
    private int numRooms;
    private int curRoom;
    public RoomGraph() {
        allRooms = new List<Room>();
        numRooms = 0;
        curRoom = 0;
    }
    //Could do a 2D float array of all entrance/exit positions in form of {exitNum, xPos, yPos, zPos}
    public void AddRoom(int pos, string name, GameObject tmParent, List<Vector3> ex) {
        allRooms.Add(new Room(pos, name, tmParent, ex));
        numRooms++;
    }

    public void AddConnect(int firstRoom, int secondRoom, int room1Pos, int room2Pos) {
        allRooms[firstRoom].AddExit(secondRoom, room1Pos, room2Pos);
        allRooms[secondRoom].AddExit(firstRoom, room2Pos, room1Pos);
    }

    public int GetNumRooms() {
        return numRooms;
    }

    public Room GetRoom(int i) {
        return allRooms[i];
    }

    public Room GetCurRoom() {
        return allRooms[curRoom];
    }

    public void SetCurRoom(int roomNum) {
        curRoom = roomNum;
    }
}


