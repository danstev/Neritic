﻿using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    //Room contains room information, mapgen will fill in this information, store these in an array and then generate the map from the information is used. Generate each room, generate corridors, maybe no complex array, just 1d

    public int x1; //Only square rooms for now, have idea for curves later.
    public int x2;
    public int y1;
    public int y2;

    public int level; //What kind of level it is
    public int monsters; //How many monsters to put in
    public int special; //Boss room? Spawn room? Treasure? etc
    public bool roofed; //Is there a roof on here? 
    public float lightlvl; //how light is the room? probably not neeeded atm.
    public int wallHeight;
    

    public GameObject floor;
    public GameObject wall;
    public GameObject roof;
    public GameObject monster;
    public GameObject npc;
    public GameObject items;

    public void generateRoom()
    {
        generateFloor();
        generateWalls();
        generateRoof();
        generateNPC();
    }

    private void generateFloor()
    {
        for(int i = x1; i < x2; i++)
        {
            for (int y = y1; y < y2; y++)
            {
                Instantiate(floor, new Vector3(i,1,y), Quaternion.identity);
            }
        }

    }

    private void generateWalls()
    {
        for (int i = x1; i < x2; i++)
        {
            for (int u = 0; u < wallHeight; u++)
            {
                Instantiate(wall, new Vector3(i, u, y1 - 1), Quaternion.identity);
                Instantiate(wall, new Vector3(i, u, y2 + 1), Quaternion.identity);
            }
        }

        for (int i = y1; i < y2; i++)
        {
            for (int u = 0; u < wallHeight; u++)
            {
                Instantiate(wall, new Vector3(x1 - 1, 1, i), Quaternion.identity);
                Instantiate(wall, new Vector3(x2 + 1, 1, i), Quaternion.identity);
            }
        }

    }

    private void generateNPC()
    {

    }

    private void generateRoof()
    {

    }

    public bool checkIntersect(Room r) //SHOULD BE A BETTER WAY OF DOING THIS : P
    {
        //Check x1, x2, y1, y2 
        //2 checks??
        bool testOne = false; //
        bool testTwo = false;

        if( r.x1 > x1 && r.x1 < x2 )
        {
            testOne = true;
        }
        else
        {
            testOne = false;
        }

        if (r.y1 > y1 && r.y1 < y2)
        {
            testTwo = true;
        }
        else
        {
            testTwo = false;
        }

        if( testOne && testTwo)
        {
            return true;
        }
        else
        {
            testOne = false;
            testTwo = false;
        }

        return false;

    }


}
