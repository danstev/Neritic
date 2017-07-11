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
    private GameObject exitLevel;
    private int[,] map;
    private Tile[] tileMap;
    private int height;
    private int width;
    private int boxMap;
    public float spaceMod;
    public Tile t;
    private float xStart = 0f;
    private float yStart = 0f;


    public void genMap()
    {
        exitLevel = Resources.Load("Prefabs/Entities/OutPortal") as GameObject;
        if (level == "dream") //tutorial level
        {
            tile = Resources.Load("Prefabs/Tiles/dreamFloorTile") as GameObject;
            wall = Resources.Load("Prefabs/Tiles/dreamWallTile") as GameObject;
            roof = Resources.Load("Prefabs/Tiles/dreamCeilingTile") as GameObject;
            foley1 = Resources.Load("Prefabs/Foley/dreamFoley1") as GameObject;
            foley2 = Resources.Load("Prefabs/Foley/dreamFoley2") as GameObject;
            foley3 = Resources.Load("Prefabs/Foley/dreamFoley3") as GameObject;
            enemTest = Resources.Load("Prefabs/NPC/marsh") as GameObject;
            enemy2 = Resources.Load("Prefabs/NPC/shadow") as GameObject;
            enemy3 = Resources.Load("Prefabs/NPC/marsh") as GameObject;
            boss = Resources.Load("Prefabs/dreamBoss") as GameObject;
            torch = Resources.Load("Prefabs/dreamTorch") as GameObject;
            genDream();
        }
        else if (level == "forest") //easy level, outside
        {
            tile = Resources.Load("Prefabs/Tiles/forestFloor") as GameObject;
            wall = Resources.Load("Prefabs/Tiles/forestWall") as GameObject;
            roof = Resources.Load("Prefabs/Tiles/forestCeiling") as GameObject;
            foley1 = Resources.Load("Prefabs/Foley/bush") as GameObject;
            foley2 = Resources.Load("Prefabs/Foley/flowers") as GameObject;
            foley3 = Resources.Load("Prefabs/Foley/grassFoleey") as GameObject;
            enemTest = Resources.Load("Prefabs/NPC/slimeTest") as GameObject;
            enemy2 = Resources.Load("Prefabs/NPC/treant") as GameObject;
            enemy3 = Resources.Load("Prefabs/NPC/shadow") as GameObject;
            boss = Resources.Load("Prefabs/dreamBoss") as GameObject;
            torch = Resources.Load("Prefabs/dreamTorch") as GameObject;
            genForest();
        }
        else if (level == "cave") //Harder, can die easily
        {
            tile = Resources.Load("Prefabs/dreamFloor") as GameObject;
            wall = Resources.Load("Prefabs/dreamWall") as GameObject;
            roof = Resources.Load("Prefabs/dreamRoof") as GameObject;
            foley1 = Resources.Load("Prefabs/dreamFoley1") as GameObject;
            foley2 = Resources.Load("Prefabs/dreamFoley2") as GameObject;
            foley3 = Resources.Load("Prefabs/dreamFoley3") as GameObject;
            enemTest = Resources.Load("Prefabs/NPC/slimeTest") as GameObject;
            enemy2 = Resources.Load("Prefabs/NPC/slimeTest") as GameObject;
            enemy3 = Resources.Load("Prefabs/NPC/slimeTest") as GameObject;
            boss = Resources.Load("Prefabs/dreamBoss") as GameObject;
            torch = Resources.Load("Prefabs/dreamTorch") as GameObject;
            //genCave();
        }
        else if (level == "dungeon") //Very hard
        {
            tile = Resources.Load("Prefabs/Tiles/dungeonFloor") as GameObject;
            wall = Resources.Load("Prefabs/Tiles/dungeonWall") as GameObject;
            roof = Resources.Load("Prefabs/Tiles/dungeonCeiling") as GameObject;
            foley1 = Resources.Load("Prefabs/Foley/dreamFoley3") as GameObject;
            foley2 = Resources.Load("Prefabs/Foley/skull") as GameObject;
            foley3 = Resources.Load("Prefabs/Foley/dust") as GameObject;
            enemTest = Resources.Load("Prefabs/NPC/beholder") as GameObject;
            enemy2 = Resources.Load("Prefabs/NPC/slimeTest") as GameObject;
            enemy3 = Resources.Load("Prefabs/NPC/shadow") as GameObject;
            boss = Resources.Load("Prefabs/dreamBoss") as GameObject;
            torch = Resources.Load("Prefabs/dreamTorch") as GameObject;
            genDungeon();
        }
        else if (level == "endLevel") //??? not planned yet, maybe underwater flowerbed? Very hard, boss level
        {
            tile = Resources.Load("Prefabs/Tiles/flowerbedFloor") as GameObject;
            wall = Resources.Load("Prefabs/Tiles/flowerbedWall") as GameObject;
            roof = Resources.Load("Prefabs/Tiles/flowerbedCeiling") as GameObject;
            foley1 = Resources.Load("Prefabs/Foley/pentagram") as GameObject;
            foley2 = Resources.Load("Prefabs/Foley/palegrass") as GameObject;
            foley3 = Resources.Load("Prefabs/Foley/coral") as GameObject;
            enemTest = Resources.Load("Prefabs/NPC/beholder") as GameObject;
            enemy2 = Resources.Load("Prefabs/NPC/smile") as GameObject;
            enemy3 = Resources.Load("Prefabs/NPC/treant") as GameObject;
            boss = Resources.Load("Prefabs/dreamBoss") as GameObject;
            torch = Resources.Load("Prefabs/dreamTorch") as GameObject;
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
        xStart = r.sx * spaceMod;
        yStart = r.sy * spaceMod;
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
        xStart = r.sx * spaceMod;
        yStart = r.sy * spaceMod;
    }

    void genDungeon()
    {
        Dungeon r = new Dungeon();
        boxMap = UnityEngine.Random.Range(60, 64);
        map = new int[boxMap, boxMap];
        r.setMap(map);
        map = r.genMap();
        tileMap = fillTileMap(map);
        renderMap(tileMap);
        xStart = r.sx * spaceMod;
        yStart = r.sy * spaceMod;
    }

    /*
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
        xStart = r.sx;
        yStart = r.sy;
    }*/

    void genUnderwater()
    {
        //spaceMod = 0.5f;
        UnderWaterFlowerbed r = new UnderWaterFlowerbed();

        map = new int[75, 75];
        r.setMap(map);
        map = r.genMap();
        tileMap = fillTileMap(map);
        renderMap(tileMap);
        xStart = r.sx * spaceMod;
        yStart = r.sy * spaceMod;
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
                    t.ceilingTile = roof;
                    t.wallTile = wall;
                    t.spacemod = spaceMod;
                    float e = UnityEngine.Random.Range(0f, 1f);
                    if (e > 0.97f)
                    {
                        //enemy spawn
                        t.entity = true;
                        t.entityObject = enemTest;
                        int f = UnityEngine.Random.Range(1, 4);
                        if (f == 1)
                        {
                            t.entityObject = enemTest;
                        }
                        else if (f == 2)
                        {
                            t.entityObject = enemy2;
                        }
                        else if (f > 2)
                        {
                            t.entityObject = enemy3;
                        }
                    }

                    if (e > 0.80f && e < 0.95f)
                    {
                        //Spawn foley
                        t.entity = true;
                        int f = UnityEngine.Random.Range(1, 4);
                        if (f == 1)
                        {
                            t.entityObject = foley1;
                            
                        }
                        else if (f == 2)
                        {
                            t.entityObject = foley2;
                        }
                        else if (f > 2)
                        {
                            t.entityObject = foley3;
                        }
                    }

                    listOfTiles.Add(t);
                }
                else if (intMap[x, y] == 2)
                {
                    Tile t = new Tile();
                    t.xPosition = x;
                    t.yPosition = y;
                    t.floorTile = tile;
                    t.ceilingTile = roof;
                    t.wallTile = wall;
                    t.spacemod = spaceMod;
                    listOfTiles.Add(t);
                }
                else if (intMap[x, y] == 3)
                {
                    
                    Tile t = new Tile();
                    t.xPosition = x;
                    t.yPosition = y;
                    t.floorTile = tile;
                    t.ceilingTile = roof;
                    t.wallTile = wall;
                    t.spacemod = spaceMod;
                    t.entity = true;
                    t.entityObject = exitLevel;
                    listOfTiles.Add(t);
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

        Vector3 mapPos = new Vector3(t.xPosition * t.spacemod, 0 * t.spacemod, t.yPosition * t.spacemod);
        GameObject j = Instantiate(t.floorTile, mapPos, Quaternion.identity) as GameObject;
        j.transform.localScale = new Vector3(1 * t.spacemod, 1 * t.spacemod, 1 * t.spacemod);
        j.transform.rotation *= Quaternion.Euler(90, 0, 0);
        j.isStatic = true;

        Vector3 mapPosCeiling = new Vector3(t.xPosition * t.spacemod, 2 * t.spacemod, t.yPosition * t.spacemod);
        GameObject h = Instantiate(t.ceilingTile, mapPosCeiling, Quaternion.identity) as GameObject;
        h.transform.localScale = new Vector3(1 * t.spacemod, 1 * t.spacemod, 1 * t.spacemod);
        h.transform.rotation *= Quaternion.Euler(270, 0, 0);
        h.isStatic = true;


        if(t.entity == true)
        {
            Vector3 pos = new Vector3(t.xPosition * t.spacemod + ( t.spacemod * UnityEngine.Random.Range(0f,1f) / 2), 1.5f * t.spacemod, t.yPosition * t.spacemod + (t.spacemod * UnityEngine.Random.Range(0f, 1f) / 2));
            GameObject o = Instantiate(t.entityObject, pos, t.entityObject.transform.rotation) as GameObject;
            o.transform.localScale = new Vector3(1 * t.spacemod, 1 * t.spacemod, -1);
        }

        renderWalls(t.xPosition, t.yPosition, t.wallTile, t.spacemod);
    }

    /*public void renderTile(Tile t)
    {

        Vector3 mapPos = new Vector3(t.xPosition * t.spacemod, 0, t.yPosition * t.spacemod);
        GameObject j = Instantiate(t.floorTile, mapPos, Quaternion.identity) as GameObject;
        j.transform.localScale = new Vector3(1 * t.spacemod, 1 * t.spacemod, 1);
        j.transform.rotation *= Quaternion.Euler(90, 0, 0);

        Vector3 mapPosCeiling = new Vector3(t.xPosition * t.spacemod, 2 * t.spacemod, t.yPosition * t.spacemod);
        GameObject h = Instantiate(t.ceilingTile, mapPosCeiling, Quaternion.identity) as GameObject;
        h.transform.localScale = new Vector3(1 * t.spacemod, 1 * t.spacemod, 1);
        h.transform.rotation *= Quaternion.Euler(270, 0, 0);

        if (t.entity == true)
        {
            Vector3 pos = new Vector3(t.xPosition * t.spacemod + (t.spacemod * UnityEngine.Random.Range(0f, 1f) / 2), 1.5f, t.yPosition * t.spacemod + (t.spacemod * UnityEngine.Random.Range(0f, 1f) / 2));
            GameObject o = Instantiate(t.entityObject, pos, t.entityObject.transform.rotation) as GameObject;
        }

        renderWalls(t.xPosition, t.yPosition, t.wallTile, t.spacemod);
    }*/

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
                Vector3 mapPos = new Vector3(x * spacemod - (spacemod /2), spacemod, y * spacemod);
                GameObject j = Instantiate(wallType, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(1 * spacemod, 2 * spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, -90, 0);
                j.isStatic = true;
            }
            else if(map[x - 1, y] == 0)
            {
            
                Vector3 mapPos = new Vector3(x * spacemod - (spacemod / 2), spacemod, y * spacemod);
                GameObject j = Instantiate(wallType, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(1 * spacemod, 2 * spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, -90, 0);
                j.isStatic = true;
            }

            if (x == map.GetLength(0) || map[x + 1, y] == 0)
            {
                Vector3 mapPos = new Vector3(x * spacemod + (spacemod / 2), spacemod, y * spacemod);
                GameObject j = Instantiate(wallType, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(1 * spacemod, 2 * spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, 90, 0);
                j.isStatic = true;
            }

            if (y == 0 || map[x, y - 1] == 0)
            {
                Vector3 mapPos = new Vector3(x * spacemod, spacemod, y * spacemod - (spacemod / 2));
                GameObject j = Instantiate(wallType, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(1 * spacemod, 2 * spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, 180, 0);
                j.isStatic = true;
            }

        
            if (y == map.GetLength(1) || map[x, y + 1] == 0)
            {
                Vector3 mapPos = new Vector3(x * spacemod, spacemod, y * spacemod + (spacemod / 2));
                GameObject j = Instantiate(wallType, mapPos, Quaternion.identity) as GameObject;
                j.transform.localScale = new Vector3(1 * spacemod, 2 * spacemod, 2);
                j.transform.rotation *= Quaternion.Euler(0, 0, 0);
                j.isStatic = true;
            }
        }
        catch(Exception e)
        {

        }

    }

    public float getXStart()
    {
        return xStart;
    }

    public float getYStart()
    {
        return yStart;
    }


}


