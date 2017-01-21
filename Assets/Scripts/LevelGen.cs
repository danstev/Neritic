using UnityEngine;
using System.Collections;

public class LevelGen : MonoBehaviour {

    //Level generator, gets a map, then turns that into terrain, monsters etc

    //0     Empty
    //1     Floor
    //2     Wall
    //

    public GameObject tile;
    public GameObject wall;
    public GameObject roof;
    private int[,] map;
    public int height;
    public int width;
    public float spaceMod;

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

                if (map[x, y] == 2)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 0, y * spaceMod);
                    Instantiate(wall, mapPos, Quaternion.identity);
                }
            }
        }
    }


}


