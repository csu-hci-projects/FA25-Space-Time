using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGraphInitializer
{
    private static GameObject[] tileMaps;
    // private static readonly float[,,] entrancePos = {
    //     {{4,22,0},{4,8,0},{95,40,0},{95,8,0},{50,55,0}}, // room 0
    //     {{3,4,0},{97,31,0},{97,4,0},{0,0,0},{0,0,0}}, //Room 1
    //     {{3,4,0},{97,31,0},{97,4,0},{0,0,0},{0,0,0}}, //Room 2
    //     {{3,4,0},{97,31,0},{97,4,0},{0,0,0},{0,0,0}} //Room 3
    // };
    private static List<Vector3> r0EnC = new List<Vector3>{new Vector3(4,22,0),new Vector3(3,8,0),new Vector3(95,40,0),new Vector3(95,4,0),new Vector3(50,55,0)};
    private static List<Vector3> r1EnC = new List<Vector3>{new Vector3(3,4,0),new Vector3(96,30,0),new Vector3(96,4,0)};
    private static List<Vector3> r2EnC = new List<Vector3>{new Vector3(3,11,0),new Vector3(46,9,0)};
    private static List<Vector3> r3EnC = new List<Vector3>{new Vector3(3,4,0),new Vector3(46,4,0)};
    private static List<Vector3> r4EnC = new List<Vector3>{new Vector3(3,5,0),new Vector3(23,5,0),new Vector3(47,5,0)};
    private static List<Vector3> r5EnC = new List<Vector3>{new Vector3(3,5,0),new Vector3(33,5,0),new Vector3(47,5,0),new Vector3(25,66,0)};
    private static List<Vector3> r6EnC = new List<Vector3>{new Vector3(3,5,0),new Vector3(47,5,0),new Vector3(25,19,0)};
    private static List<Vector3> r7EnC = new List<Vector3>{new Vector3(3,5,0),new Vector3(25,5,0)};
    private static List<Vector3> r8EnC = new List<Vector3>{new Vector3(5,35,0),new Vector3(47,5,0),new Vector3(47,27,0)};
    private static List<Vector3> r9EnC = new List<Vector3>{new Vector3(47,35,0)};
    private static List<List<Vector3>> entrancePos = new List<List<Vector3>>{r0EnC,r1EnC,r2EnC,r3EnC,r4EnC,r5EnC,r6EnC,r7EnC,r8EnC,r9EnC};
    private static string[] names = {"Starting Room", "Dungeon 1", "Dungeon 2", "Dungeon 3", "Dungeon 4","Dungeon 5","Dungeon 6", "Dungeon 7","Dungeon 8", "Dungeon 9"};
    

    // Input order room1 ID, Room2 ID, Room1 Exit, Room 2 Exit
    public static void RoomGraphInit(RoomGraph roomGraph, GameObject[] tM) {
        tileMaps = tM;
        for(int i = 0; i < entrancePos.Count; i++) {
            roomGraph.AddRoom(i, names[i], tileMaps[i],entrancePos[i]);
        }
        AddConnections(roomGraph);
    }

    // Room 1, Room 2, Door num 1, Door num 2
    public static void AddConnections(RoomGraph roomGraph) {
        
    }
}

