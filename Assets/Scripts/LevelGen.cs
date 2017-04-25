using UnityEngine;
using System.Collections;
using System;

public class LevelGen : MonoBehaviour {

    //Level generator, gets a map, then turns that into terrain, monsters etc

    //0     Empty
    //1     Floor
    //2     Wall

    public string level;
    private GameObject tile;
    private GameObject wall;
    private GameObject roof;
    private GameObject foley1;
    private GameObject foley2;
    private GameObject foley3;
    private GameObject enemTest;
    private GameObject enemy2;
    private GameObject enemy3;
    private GameObject boss;
    private GameObject torch;
    private int[,] map;
    private Tile[] tileMap;
    private int height;
    private int width;
    private int boxMap;
    public float spaceMod;
    public Tile t;



    // Use this for initialization
    void Start () {

        if(level == "dream") //tutorial level
        {
            tile = Resources.Load("Prefabs/Blood") as GameObject;
            wall = Resources.Load("Prefabs/Blood") as GameObject;
            roof = Resources.Load("Prefabs/Blood") as GameObject;
            foley1 = Resources.Load("Prefabs/Blood") as GameObject;
            foley2 = Resources.Load("Prefabs/Blood") as GameObject;
            foley3 = Resources.Load("Prefabs/Blood") as GameObject;
            enemTest = Resources.Load("Prefabs/Blood") as GameObject;
            enemy2 = Resources.Load("Prefabs/Blood") as GameObject;
            enemy3 = Resources.Load("Prefabs/Blood") as GameObject;
            boss = Resources.Load("Prefabs/Blood") as GameObject;
            torch = Resources.Load("Prefabs/Blood") as GameObject;
            genDream();
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
            genUnderwater();
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

    void genDream()
    {
        Dream r = new Dream();
        height = UnityEngine.Random.Range(32, 46);
        width = 250;
        map = new int[height, width];
        r.setMap(map);
        map = r.genMap();
        tileMap = fillTileMap(map);
        renderMap(tileMap);
    }

    void genDungeon()
    {
        Dungeon r = new Dungeon();
        boxMap = UnityEngine.Random.Range(46, 64);
        map = new int[boxMap, boxMap];
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

    void genUnderwater()
    {
        //spaceMod = 0.5f;
        UnderWaterFlowerbed r = new UnderWaterFlowerbed();
        height = UnityEngine.Random.Range(100, 125);
        width = UnityEngine.Random.Range(100, 125);
        map = new int[height, width];
        r.setMap(map);
        map = r.genMap();
        tileMap = fillTileMap(map);
        renderMap(tileMap);
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
                    t.ceiling = true;
                    listOfTiles.Add(t);
                    float e = UnityEngine.Random.Range(0f,1f);
                    if(e > 0.95f)
                    {
                        t.entity = true;
                        t.entityObject = enemTest;
                    }

                    if(e > 0.90f && e < 0.95f)
                    {
                        //Spawn foley
                        int f = UnityEngine.Random.Range(1, 3);
                        if(f == 1)
                        {
                            t.entityObject = foley1;
                            t.entity = true;
                        }
                        else if(f == 2)
                        {
                            t.entityObject = foley2;
                            t.entity = true;
                        }
                        else if(f == 3)
                        {
                            t.entityObject = foley3;
                            t.entity = true;
                        }
                    }
                }
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

    public void renderTile(Tile t)
    {

        Vector3 mapPos = new Vector3(t.xPosition * t.spacemod, 0, t.yPosition * t.spacemod);
        GameObject j = Instantiate(t.floorTile, mapPos, Quaternion.identity) as GameObject;
        j.transform.localScale = new Vector3(1 * t.spacemod, 1 * t.spacemod, 1);
        j.transform.rotation *= Quaternion.Euler(90, 0, 0);

        if (t.ceiling)
        {
            Vector3 mapPosCeiling = new Vector3(t.xPosition * t.spacemod, 2 * t.spacemod, t.yPosition * t.spacemod);
            GameObject h = Instantiate(t.floorTile, mapPosCeiling, Quaternion.identity) as GameObject;
            h.transform.localScale = new Vector3(1 * t.spacemod, 1 * t.spacemod, 1);
            h.transform.rotation *= Quaternion.Euler(270, 0, 0);
        }

        if(t.entityObject != null)
        {
            GameObject h = Instantiate(t.entityObject, mapPos, Quaternion.identity) as GameObject;
        }

        renderWalls(t.xPosition, t.yPosition, t.floorTile, t.spacemod);
    }

    void renderMap(Tile[] t)
    {
        foreach(Tile r in t)
        {
            renderTile(r);
        }
    }

    void renderWalls(int x, int y, GameObject wallType, float spacemod)
    {
        try
        {
            if (x == 0)
            {
                Vector3 mapPos = new Vector3(x * spacemod -1, 2, y * spacemod);
                GameObject j = Instantiate(wallType, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(1 * spacemod, 2 * spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, -90, 0);
            }
            else if(map[x - 1, y] == 0)
            {
            
                Vector3 mapPos = new Vector3(x * spacemod -1, 2, y * spacemod);
                GameObject j = Instantiate(wallType, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(1 * spacemod, 2 * spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, -90, 0);
            }

            if (x == map.GetLength(0) || map[x + 1, y] == 0)
            {
                Vector3 mapPos = new Vector3(x * spacemod + 1, 2, y * spacemod);
                GameObject j = Instantiate(wallType, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(1 * spacemod, 2 * spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, 90, 0);
            }

            if (y == 0 || map[x, y - 1] == 0)
            {
                Vector3 mapPos = new Vector3(x * spacemod, 2, y * spacemod -1);
                GameObject j = Instantiate(wallType, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(1 * spacemod, 2 * spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, 180, 0);
            }

        
            if (y == map.GetLength(1) || map[x, y + 1] == 0)
            {
                Vector3 mapPos = new Vector3(x * spacemod, 2, y * spacemod + 1);
                GameObject j = Instantiate(wallType, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(1 * spacemod, 2 * spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, 0, 0);
            }
        }
        catch(Exception e)
        {

        }

    }


}


