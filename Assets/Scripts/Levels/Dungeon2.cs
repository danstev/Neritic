using UnityEngine;
using System.Collections;

public class Dungeon2 {

    private int[,] map;
    int x1, x2, y1, y2;
    public int sx, sy, ex, ey;
    int x, y;

    int[] xMid;
    int[] yMid;

    public int[,] genMap()
    {
        
        x = map.GetLength(0);
        y = map.GetLength(1);
        int rooms = 4;
        xMid = new int[rooms * rooms];
        yMid = new int[rooms * rooms];
        //Make starting room near edge.
        drawRoom(rooms);

        for(int u = 0; u < xMid.Length; u++)
        {
            if(u == 0) //first
            {
                //right down
                buildCorridor(xMid[u], yMid[u], xMid[u + 1], yMid[u + 1], 1);
                buildCorridor(xMid[u], yMid[u], xMid[u + rooms], yMid[u + rooms], 1);
            }
            else if(u == xMid.Length-1) //Last
            {
                //Above left
                buildCorridor(xMid[u], yMid[u], xMid[u - 1], yMid[u - 1], 1);
                buildCorridor(xMid[u], yMid[u], xMid[u - rooms], yMid[u - rooms], 1);
            }
            else if(u < rooms -1) //First line
            {
                //lefft right down
                buildCorridor(xMid[u], yMid[u], xMid[u - 1], yMid[u - 1], 1);
                buildCorridor(xMid[u], yMid[u], xMid[u + 1], yMid[u + 1], 1);
                buildCorridor(xMid[u], yMid[u], xMid[u + rooms], yMid[u + rooms], 1);
            }
            else if(u > (xMid.Length - rooms)) //Last line
            {
                //left right up
                buildCorridor(xMid[u], yMid[u], xMid[u - 1], yMid[u - 1], 1);
                buildCorridor(xMid[u], yMid[u], xMid[u + 1], yMid[u + 1], 1);
                buildCorridor(xMid[u], yMid[u], xMid[u - rooms], yMid[u - rooms], 1);
            }
            else if(u % rooms == 0) //First column
            {

            }
            else if (u % rooms == rooms)
            {

            }
            else
            {
                //up, down, left right
            }

        }


        map[xMid[0], yMid[0]] = 2;
        map[xMid[xMid.Length -1], yMid[yMid.Length-1]] = 3;

        return map;
    }

    private void drawRoom(int square)
    {
        int count = 0;
        for(int i = 0; i < square; i++)
        {
            for (int x = 0; x < square; x++)
            {
                int h1 = Random.Range(4, 8);
                int j1 = Random.Range(4, 8);

                int h = h1 + (i * 12);
                int j = j1 + (x * 12);

                xMid[count] = h + (h1 / 2);
                yMid[count] = j + (j1 / 2);

                for (int u = h; u < h + h1; u++)
                {
                    for (int k = j; k < j + j1; k++)
                    {
                        map[u, k] = 1;
                    }
                }
                count++;
            }
        }
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

                y1 = i;
                y2 = t;

                if (i == 3 && t == 3)
                {
                    map[i + 1, t + 1] = 2;
                    sx = i + 1;
                    sy = t + 1;
                }
                else
                {
                    map[i + 1, t + 1] = 1;
                }
            }
        }
    }

    private void buildEndRoom()
    {
        for (int o = x - 20; o < x - 5; o++)
        {
            for (int u = y - 20; u < y - 5; u++)
            {
                map[u, o] = 1;
                x1 = u;
                x2 = o;

                if (o == x - 10 && u == y - 10)
                {
                    map[u, o] = 3;
                    ex = u;
                    ey = o;
                }

            }
        }

    }

    private void buildCorridor(int x1, int x2, int y1, int y2, int tileType)
    {
        //bool dir = false; //false = horizontal, true = vertical Not implemented yet

        //check input
        if (x1 > y1)
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

        for (int h = x1; h < middle; h++)
        {
            if (map[h, x2] == 0)
            {
                map[h, x2] = tileType;
            }
        }

        for (int h = middle; h < y1; h++)
        {
            if (map[h, y2] == 0)
            {
                map[h, y2] = tileType;
            }
        }

        if (x2 < y2)
        {
            for (int h = x2; h < y2; h++)
            {
                if (map[middle, h] == 0)
                {
                    map[middle, h] = tileType;
                }
            }
        }
        else
        {
            for (int h = y2; h < x2; h++)
            {
                if (map[middle, h] == 0)
                {
                    map[middle, h] = tileType;
                }
            }
        }

        if (map[middle, x2] == 0)
        {
            map[middle, x2] = tileType;
        }
        if (map[middle, y2] == 0)
        {
            map[middle, y2] = tileType;
        }

    }

    
}
