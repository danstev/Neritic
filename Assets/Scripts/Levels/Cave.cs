﻿using UnityEngine;
using System.Collections;

public class Cave {

    private int[,] map;

    public int[,] genMap()
    {
        int x = map.GetLength(0);
        int y = map.GetLength(1);

        //Make starting room near edge.


        //Randomly select a wall

        //build corridor
        // check for room

        //Room at end of corridor
        // check for room

        //1-3 rooms from corridor based on corridor length
        // check for room
        return map;
    }

    public void setMap(int[,] m)
    {
        map = m;
    }

    private void buildStartRoom()
    {
        int c = Random.Range(4, 12); 
        for (int i = 0; i < 5; i++)
        {
            for(int t = 0; t < 5; t++)
            {

            }
        }
    }
}