using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
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
    public int wallType;

    float spacemod = 1f;

    public void render()
    {
        //INit tile at x/y
        //check ceiling
        //init ceiling
        //check torch
        //init torch
        //check entity
        //init enttiy;

        Vector3 mapPos = new Vector3(xPosition, 0, yPosition);
        GameObject j = Instantiate(floorTile, mapPos, Quaternion.identity) as GameObject;
        j.transform.localScale = new Vector3(2 * spacemod, 2 * spacemod, 1);
        //Somehow transform it 90 degrees
        j.transform.rotation *= Quaternion.Euler(90, 0, 0);
    }

}
