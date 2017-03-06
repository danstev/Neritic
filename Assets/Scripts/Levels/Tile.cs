using UnityEngine;
using System.Collections;

public class Tile
{

    public GameObject floorTile;
    
    //X position of the tile
    public int xPosition;
    //Y position of the tile
    public int yPosition;
    //Whether this tile requires a ceiling
    bool ceiling;
    GameObject ceilingTile;
    //Adds a torch (light) to this tile
    bool torch;
    GameObject torchObject;
    //Spawn entity (enemy, chest, door, exit etc) in middle of tile
    bool entity;
    GameObject entityObject;


    //0 top
    //1 right
    //2 bottom
    //3 left
    public bool wall;
    public int wallType;

    public float spacemod = 1f;

   

}
