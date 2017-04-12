﻿using UnityEngine;
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
    public int boxMap;
    public float spaceMod;
    public Tile t;



    // Use this for initialization
    void Start () {

        if(level == "dream") //tutorial level
        {
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


}


