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



    // Use this for initialization
    void Start () {
        fillMapRandomBoxes();
        drawMap();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void fillMapRandomHoles()
    {
        System.Random r = new System.Random(seed.GetHashCode());
        map = new int[width, height];

        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                map[x,y] = (r.Next(0,100) < fillPercent) ? 1 : 0;
            }
        }
    }

    void fillMapRandomBoxes()
    {
        System.Random r = new System.Random(seed.GetHashCode());
        map = new int[width, height];
        int[,] rooms = new int[amountOfRooms, 4];

        for (int i = 0; i < amountOfRooms; i++)
        {
            rooms[i, 0] = r.Next(0, height - 8);
            rooms[i, 1] = r.Next(3, 8) + rooms[i, 0];
            rooms[i, 2] = r.Next(0, width - 8);
            rooms[i, 3] = r.Next(3, 8) + rooms[i, 2];
        }

        for (int i = 0; i < amountOfRooms; i++)
        {
            int[] room = new int[4];
            room[0] = rooms[i, 0]; ///////////////REDO THIS INTO ITS OWN CLASS : (
            room[1] = rooms[i, 1];
            room[2] = rooms[i, 2];
            room[3] = rooms[i, 3];

            for (int x = rooms[i,0]; x < rooms[i,1]; x++)
            {
                for(int y = rooms[i, 2]; y < rooms[i, 3]; y++)
                {
                    map[x, y] = 1;

                }
            }
        }
    }

    bool roomIntersect(int[] room1, int[] room2)
    {
        if(room1[0] <= room2[1] && room1[1] <= room2[0] && room1[2] <= room2[3] && room1[3] <= room2[2])
        {
            return true;
        }
        return false;
    }

    void fillEdges()
    {
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if( x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
            }
        }
    }

    void drawMap()
    {
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if (map[x, y] == 1)
                {
                    Vector3 mapPos = new Vector3(x * spaceMod, 0, y * spaceMod);
                    Instantiate(tile, mapPos, Quaternion.identity);
                }
            }
        }
    }
}
