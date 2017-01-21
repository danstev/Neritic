using UnityEngine;
using System.Collections;

public class LevelGen : MonoBehaviour {

    public GameObject tile;
    private int[,] map;
    public int height;
    public int width;
    public float spaceMod;

    //RandomHoles
    public string seed;
    public int fillPercent;

    //Rooms
    public int amountOfRooms;

    public Dungeon d;



    // Use this for initialization
    void Start () {
        height = Random.Range(46, 64);
        width = Random.Range(46, 64);
        map = new int[height,width];
        d.setMap(map);
        map = d.genMap();
        drawMap();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void drawMap()
    {
        for(int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if(map[x,y] == 1)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 0, y * spaceMod);
                    Instantiate(tile, mapPos, Quaternion.identity);
                }
            }
        }

    }


}


