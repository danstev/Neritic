﻿using UnityEngine;
using System.Collections;
using System;

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
    private Tile[] tileMap;
    public int height;
    public int width;
    public float spaceMod;
    public Tile t;



    // Use this for initialization
    void Start () {

        genDungeon();

    }

    void genForest()
    {
        Forest r = new Forest();
        height = UnityEngine.Random.Range(46, 64);
        width = UnityEngine.Random.Range(46, 64);
        map = new int[height, width];
        r.setMap(map);
        map = r.genMap();
        drawMapForest();
    }

    void genDungeon()
    {
        Dungeon r = new Dungeon();
        height = UnityEngine.Random.Range(46, 64);
        width = UnityEngine.Random.Range(46, 64);
        map = new int[height, width];
        r.setMap(map);
        map = r.genMap();
        ();
    }

    void genCave()
    {
        Cave c = new Cave();
        height = UnityEngine.Random.Range(46, 64);
        width = UnityEngine.Random.Range(46, 64);
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
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == 1)
                {
                    Tile t = new Tile();
                    t.xPosition = x;
                    t.yPosition = y;
                    t.floorTile = tile;
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
        //INit tile at x/y
        //check ceiling
        //init ceiling
        //check torch
        //init torch
        //check entity
        //init enttiy;

        Vector3 mapPos = new Vector3(t.xPosition, 0, t.yPosition);
        GameObject j = Instantiate(t.floorTile, mapPos, Quaternion.identity) as GameObject;
        j.transform.localScale = new Vector3(2 * t.spacemod, 2 * t.spacemod, 1);
        //Somehow transform it 90 degrees
        j.transform.rotation *= Quaternion.Euler(90, 0, 0);
    }

    void renderMap(Tile[] t)
    {
        foreach(Tile r in t)
        {
            renderTile(t);
        }
    }


}


