using UnityEngine;
using System.Collections;

public class Tile {

    GameObject floorTile;
    
    //X position of the tile
    int xPosition;
    //Y position of the tile
    int yPosition;
    //Whether this tile requires a ceiling
    bool ceiling;
    GameObject ceilingTile;
    //Adds a torch (light) to this tile
    bool torch;
    GameObject torchObject;
    //Spawn entity (enemy, chest, door, exit etc) in middle of tile
    bool entity;
    GameObject entityObject;



}
