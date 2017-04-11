using UnityEngine;
using System.Collections;

public class Dream {

    private int[,] map;
    int x1, x2, y1, y2;

    public int[,] genMap()
    {
        x1 = 0;
        x2 = 0;
        int x = map.GetLength(0);
        int y = map.GetLength(1);

        //Gen start room
        buildStartRoom();
        int offsetW = Random.Range(7, 15);
        for (int i = 0; i < 10; i++)
        {
            int height = Random.Range(4, 12);
            int width = Random.Range(4, 12);
            int offsetH = Random.Range(0, x - height);
            offsetW += 10;


            for (int q = offsetH; q <= height + offsetH; q++)
            {
                for (int w = offsetW; w <= width + offsetW; w++)
                {
                    map[q, w] = 1;

                    if (w == width + offsetW - 1)
                    {
                        x1 = q;
                        x2 = w;
                        if (y1 == -1)
                        {
                            y1 = x1;
                            y2 = x2;
                        }
                        else
                        {
                            buildCorridor(x1, x2, y1, y2, 1);
                            y1 = x1;
                            y2 = x2;
                        }
                    }
                }
            }

        }
        //Gen a bunch of rooms which lead to end room, no offshoots
        //Gen end room (large with boss)

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
                y1 = i;
                y2 = t;
            }
        }
    }

    private void buildEndRoom()
    {
        int c = Random.Range(map.GetLength(0) - 5, map.GetLength(1) - 5);
        for (int i = 0; i < 4; i++)
        {
            for (int t = 0; t < 4; t++)
            {
                map[map.GetLength(0) - 8 + i, map.GetLength(1) - 8 + t] = 1;
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
            map[h, x2] = tileType;
        }

        for (int h = middle; h < y1; h++)
        {
            map[h, y2] = tileType;
        }

        if (x2 < y2)
        {
            for (int h = x2; h < y2; h++)
            {
                map[middle, h] = tileType;
            }
        }
        else
        {
            for (int h = y2; h < x2; h++)
            {
                map[middle, h] = tileType;
            }
        }

        map[middle, x2] = tileType;
        map[middle, y2] = tileType;

    }
}
