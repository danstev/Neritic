using UnityEngine;
using System.Collections;

public class Room {

    //Room contains room information, mapgen will fill in this information, store these in an array and then generate the map from the information is used.

    public int x1; //Only square rooms for now, have idea for curves later.
    public int x2;
    public int y1;
    public int y2;

    public int level; //What kind of level it is
    public int monsters; //How many monsters to put in
    public int special; //Boss room? Spawn room? Treasure? etc
    public bool roofed; //Is there a roof on here? 
    public float lightlvl; //how light is the room? probably not neeeded atm.


}
