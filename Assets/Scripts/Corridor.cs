using UnityEngine;
using System.Collections;

public class Corridor : MonoBehaviour {

    //The level generator has rooms and corridors which it creates when needed.
    //This is the corridor.
    //Basically a fancy, has 3 coordinates, draw a floor between them all.
    //Walls handeled by upper level?

    public GameObject floor;

    public int x1;
    public int x2;
    public int y1;
    public int y2;
    public int z1;
    public int z2;

    //Check for an intersection of the corridor with the rooms
    bool checkIntersect()
    {
        return false;
    }

    //Draws the corridor
    void drawCorridor()
    {
        //Need to draw between 3 coordinates but not be able to but need to be able to draw from either a top or bottom


    }


}
