using UnityEngine;
using System.Collections;

public class LevelGen : MonoBehaviour {

    //Level generator, gets a map, then turns that into terrain, monsters etc

    //0     Empty
    //1     Floor
    //2     Wall
    //10    Tree

    public GameObject tile;
    public GameObject wall;
    public GameObject roof;
    public GameObject tree;
    private int[,] map;
    public int height;
    public int width;
    public float spaceMod;



    // Use this for initialization
    void Start () {

        genCave();

    }

    void genForest()
    {
        Forest r = new Forest();
        height = Random.Range(46, 64);
        width = Random.Range(46, 64);
        map = new int[height, width];
        r.setMap(map);
        map = r.genMap();
        drawMapForest();
    }

    void genDungeon()
    {
        Dungeon r = new Dungeon();
        height = Random.Range(46, 64);
        width = Random.Range(46, 64);
        map = new int[height, width];
        r.setMap(map);
        map = r.genMap();
        drawMapDungeon();
    }

    void genCave()
    {
        Cave c = new Cave();
        height = Random.Range(46, 64);
        width = Random.Range(46, 64);
        map = new int[height, width];
        c.setMap(map);
        map = c.genMap();
        drawMapDungeon();
    }

    // Update is called once per frame
    void Update () {
	
	}

    void drawMapDungeon()
    {
        
        for(int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if(map[x,y] == 1)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 0 + Random.Range(0, 0.25f), y * spaceMod);
                    GameObject j = Instantiate(tile, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod, spaceMod, spaceMod);
                }

                if (map[x, y] == 2)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 1, y * spaceMod);
                    GameObject j = Instantiate(wall, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod, spaceMod * 2, spaceMod);
                }
            }
        }
    }

    void drawMapForest()
    {

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == 0)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 0 + Random.Range(0, 0.25f), y * spaceMod);
                    GameObject j = Instantiate(tile, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod, spaceMod, spaceMod);
                }

                if (map[x, y] == 10)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 1, y * spaceMod);
                    Quaternion q = new Quaternion(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                    GameObject j = Instantiate(tree, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod + Random.Range(0.0f, 0.5f), spaceMod * 2 + Random.Range(0.0f, 0.5f), spaceMod + Random.Range(0.0f, 0.5f));
                    j.transform.rotation = q;
                }
            }
        }
    }

    void drawMap()
    {

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == 0)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 0 + Random.Range(0, 0.25f), y * spaceMod);
                    GameObject j = Instantiate(tile, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod, spaceMod, spaceMod);
                }
                else if (map[x, y] == 10)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 1, y * spaceMod);
                    Quaternion q = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                    GameObject j = Instantiate(tree, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod + Random.Range(0.0f, 0.5f), spaceMod * 2 + Random.Range(0.0f, 0.5f), spaceMod + Random.Range(0.0f, 0.5f));
                    j.transform.rotation = q;
                }
            }
        }
    }


}


