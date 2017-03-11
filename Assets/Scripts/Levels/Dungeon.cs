using UnityEngine;
using System.Collections;

public class Dungeon {

    private int[,] map;

    public int[,] genMap()
    {
        int x = map.GetLength(0);
        int y = map.GetLength(1);
        int rooms = Random.Range(8,12);
        //Make starting room near edge.
        buildStartRoom();
        buildEndRoom();

        
        for (int i = 0; i < rooms; i++)
        {
            int height = Random.Range(4, 12);
            int width = Random.Range(4, 12);
            int offsetH = Random.Range(0, x - height - 1);
            int offsetW = Random.Range(0, y - width - 1);


            for (int q = offsetH; q < height + offsetH; q++)
            {
                for (int w = offsetW; w < width + offsetW; w++)
                {
                    map[q, w] = 1;
                }
            }
        }
        

        return map;
    }

    public void setMap(int[,] m)
    {
        map = m;
    }

    private void buildStartRoom()
    {
        int c = Random.Range(4, 12);
        for (int i = 0; i < 5; i++)
        {
            for (int t = 0; t < 5; t++)
            {
                map[i, t] = 1;
            }
        }
    }

    private void buildEndRoom()
    {
        int c = Random.Range(map.GetLength(0) - 4, map.GetLength(1) - 12);
        for (int i = 0; i < 5; i++)
        {
            for (int t = 0; t < 5; t++)
            {
                map[c + i, c + t] = 1;
            }
        }
    }

    private int[] selectWall()
    {
        int[] coord = new int[2];


        return coord;
    }

    private bool checkIfWalls(int[] coords)
    {
        if (map[coords[0], coords[1]] == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int[,] startToEnd(int[,] m)
    {
        int[,] corridor = m;

        return corridor;
    }

    private void buildCorridor(int x1, int x2, int y1, int y2)
    {
        //x1,x2 coord 1
        //y1,y2 coord 2
        //
        bool dir = false; //false = horizontal, true = vertical
        
        //check input
        if(x1 > y1)
        {
            int t1, t2;
            t1 = x1;
            t2 = x2;

            x1 = y1;
            x2 = y2;

            y1 = t1;
            y2 = t2;
        }

        int middle = (x1 + y1) / 2;

        for(int h = x1; h < middle; h++)
        {
            map[h, x2] = 1;
        }

        for (int h = middle; h < y1; h++)
        {
            map[h, y2] = 1;
        }

        if(x2 < y2)
        {
            for (int h = x2; h < y2; h++)
            {
                map[middle, h] = 1;
            }
        }
        else
        {
            for (int h = y2; h < x2; h++)
            {
                map[middle, h] = 1;
            }
        }
        

    }

}
