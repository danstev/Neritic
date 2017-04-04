using UnityEngine;
using System.Collections;
using System;

public class LevelGen : MonoBehaviour {

    //Level generator, gets a map, then turns that into terrain, monsters etc

    //0     Empty
    //1     Floor
    //2     Wall

    public string level;
    public GameObject tile;
    public GameObject wall;
    public GameObject roof;
    public GameObject tree;
    private int[,] map;
    private Tile[] tileMap;
    public int height;
    public int width;
    public float spaceMod;
    public Tile t;



    // Use this for initialization
    void Start () {

        if(level == "dream") //tutorial level
        {
            genDungeon();
        }
        else if (level == "forest") //easy level, outside
        {
            genForest();
        }
        else if(level == "cave") //Harder, can die easily
        {
            genCave();
        }
        else if(level == "dungeon") //Very hard
        {
            genDungeon();
        }
        else if(level == "endLevel") //??? not planned yet, maybe underwater flowerbed? Very hard, boss level
        {
            genDungeon();
        }

        

    }

    void genForest()
    {
        Forest r = new Forest();
        height = UnityEngine.Random.Range(46, 64);
        width = UnityEngine.Random.Range(46, 64);
        map = new int[height, width];
        r.setMap(map);
        map = r.genMap();
        tileMap = fillTileMap(map);
        renderMap(tileMap);
    }

    void genDungeon()
    {
        Dungeon r = new Dungeon();
        height = UnityEngine.Random.Range(46, 64);
        width = UnityEngine.Random.Range(46, 64);
        map = new int[height, width];
        r.setMap(map);
        map = r.genMap();
        tileMap = fillTileMap(map);
        renderMap(tileMap);
    }

    void genCave()
    {
        Cave r = new Cave();
        height = UnityEngine.Random.Range(46, 64);
        width = UnityEngine.Random.Range(46, 64);
        map = new int[height, width];
        r.setMap(map);
        map = r.genMap();
        tileMap = fillTileMap(map);
        renderMap(tileMap);
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
                    Vector3 mapPos = new Vector3(x * spaceMod, 0 + UnityEngine.Random.Range(0, 0.25f), y * spaceMod);
                    GameObject j = Instantiate(tile, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod, spaceMod, spaceMod);

                    Vector3 mapPos2 = new Vector3(x * spaceMod, 0 + UnityEngine.Random.Range(5, 5.25f), y * spaceMod);
                    GameObject i = Instantiate(tile, mapPos2, Quaternion.identity) as GameObject;
                    i.transform.localScale = new Vector3(spaceMod, spaceMod, spaceMod);
                }

                if (map[x, y] == 2)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 1, y * spaceMod);
                    GameObject j = Instantiate(wall, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod, spaceMod * 4, spaceMod);
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
                    Vector3 mapPos = new Vector3(x * spaceMod, 0 + UnityEngine.Random.Range(0, 0.25f), y * spaceMod);
                    GameObject j = Instantiate(tile, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod, spaceMod, spaceMod);
                }

                if (map[x, y] == 10)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 1, y * spaceMod);
                    Quaternion q = new Quaternion(UnityEngine.Random.Range(0,360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
                    GameObject j = Instantiate(tree, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod + UnityEngine.Random.Range(0.0f, 0.5f), spaceMod * 2 + UnityEngine.Random.Range(0.0f, 0.5f), spaceMod + UnityEngine.Random.Range(0.0f, 0.5f));
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
                    Vector3 mapPos = new Vector3(x * spaceMod, 0 + UnityEngine.Random.Range(0, 0.25f), y * spaceMod);
                    GameObject j = Instantiate(tile, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod, spaceMod, spaceMod);
                }
                else if (map[x, y] == 10)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 1, y * spaceMod);
                    Quaternion q = new Quaternion(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
                    GameObject j = Instantiate(tree, mapPos, Quaternion.identity) as GameObject;
                    j.transform.localScale = new Vector3(spaceMod + UnityEngine.Random.Range(0.0f, 0.5f), spaceMod * 2 + UnityEngine.Random.Range(0.0f, 0.5f), spaceMod + UnityEngine.Random.Range(0.0f, 0.5f));
                    j.transform.rotation = q;
                }
            }
        }
    }

    Tile[] fillTileMap(int[,] intMap)
    {
        ArrayList listOfTiles = new ArrayList();
        for (int x = 0; x < intMap.GetLength(0); x++)
        {
            for (int y = 0; y < intMap.GetLength(1); y++)
            {
                if (intMap[x, y] == 1)
                {
                    Tile t = new Tile();
                    t.xPosition = x;
                    t.yPosition = y;
                    t.floorTile = tile;
                    t.spacemod = spaceMod;
                    listOfTiles.Add(t);
                }
                else if(map[x, y] == 2)
                {
                    Tile t = new Tile();
                    t.xPosition = x;
                    t.yPosition = y;
                    t.floorTile = wall;
                    t.spacemod = spaceMod;
                    t.wall = true;
                    t.wallType = getWallType(intMap,x,y);
                    listOfTiles.Add(t);
                }
                //else if (map[x, y] == 0) //Maybe not needed?
                //{
                //    if(map[x-1, y] != 0)
                //    {

                //    }
                //}
            }
        }
        Tile[] tiles = new Tile[listOfTiles.Count];
        int count = 0;
        foreach(Tile h in listOfTiles)
        {
            tiles[count] = h;
            count++;
        }
        return tiles;
    }

    int getWallType(int[,] intMap, int xPos, int yPos)
    {
        if(xPos == 0 || yPos == 0 || xPos + 1 == height || yPos + 1 == width)
        {
            return 5;
        }
        else if (intMap[xPos + 1, yPos] != 0)
        {
            print(intMap[xPos + 1, yPos]);
            return 1;
        }
        else if(intMap[xPos - 1, yPos] != 0)
        {
            return 3;
        }
        else if (intMap[xPos, yPos - 1] != 0)
        {
            return 0;
        }
        else if (intMap[xPos, yPos + 1] != 0)
        {
            return 2;
        }
        else
        {
            return 5;
        }
    }

    public void renderTile(Tile t)
    {
        //INit tile at x/y
        //check ceiling
        //init ceiling
        //check torch
        //init torch
        //check entity
        //init enttiy;
        if (t.wall)
        {
            if(t.wallType == 0)
            {
                Vector3 mapPos = new Vector3(t.xPosition * t.spacemod, 2, t.yPosition * t.spacemod - 1);
                GameObject j = Instantiate(t.floorTile, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(2 * t.spacemod, 1 * t.spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, 0, 0);
            }
            else if(t.wallType == 1)
            {
                Vector3 mapPos = new Vector3(t.xPosition * t.spacemod + 1, 2, t.yPosition * t.spacemod);
                GameObject j = Instantiate(t.floorTile, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(2 * t.spacemod, 1 * t.spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, 0, 90);
            }
            else if (t.wallType == 2)
            {
                Vector3 mapPos = new Vector3(t.xPosition * t.spacemod, 2, t.yPosition * t.spacemod + 1);
                GameObject j = Instantiate(t.floorTile, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(2 * t.spacemod, 1 * t.spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, 0, 180);

            }
            else if (t.wallType == 3)
            {
                Vector3 mapPos = new Vector3(t.xPosition * t.spacemod - 1, 2, t.yPosition * t.spacemod);
                GameObject j = Instantiate(t.floorTile, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(2 * t.spacemod, 1 * t.spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, 0, 270);
            }
        }
        else
        {
            Vector3 mapPos = new Vector3(t.xPosition * t.spacemod, 0, t.yPosition * t.spacemod);
            GameObject j = Instantiate(t.floorTile, mapPos, Quaternion.identity) as GameObject;
            j.transform.localScale = new Vector3(1 * t.spacemod, 1 * t.spacemod, 1);
            //Somehow transform it 90 degrees
            j.transform.rotation *= Quaternion.Euler(90, 0, 0);

            Vector3 mapPosCeiling = new Vector3(t.xPosition * t.spacemod, 2 * t.spacemod, t.yPosition * t.spacemod);
            GameObject h = Instantiate(t.floorTile, mapPosCeiling, Quaternion.identity) as GameObject;
            h.transform.localScale = new Vector3(1 * t.spacemod, 1 * t.spacemod, 1);
            h.transform.rotation *= Quaternion.Euler(270, 0, 0);
        }
    }

    void renderMap(Tile[] t)
    {
        foreach(Tile r in t)
        {
            renderTile(r);
        }
    }


}


