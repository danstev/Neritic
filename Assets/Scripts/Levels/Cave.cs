﻿using UnityEngine;
using System.Collections;

public class Cave {

    //Gen rooms, put walls around rooms, generate doors based on walls, generate corridors based on walls as doors, generate walls again for corridors

    private int[,] map;

    public int[,] genMap()
    {
        int x = map.GetLength(0);
        int y = map.GetLength(1);
        int rooms = 10;//(x * y) / 30; //Change 30 to a different number maybe to increase/decrease density
        //int[,] points = new int[rooms * 4, rooms * 4];

        //Generate rooms
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

        //NEEDS CORRIDORS
        randomEachSquare();
        smoothMap(6);
        generateWalls();
        return map;
    }

    public void setMap(int[,] m)
    {
        map = m;
    }

    private void smoothMap(int smoothness)
    {
        int x = map.GetLength(0);
        int y = map.GetLength(1);

        for (int i = 0; i < smoothness; i++)
        {
            for (int g = 0; g < x; g++)
            {
                for (int h = 0; h < y; h++)
                {
                    int count = 0;

                    if (g - 1 < 0 || g + 1 >= map.GetLength(0) || h - 1 < 0 || h + 1 >= map.GetLength(1))
                    {
                        map[g, h] = 1;
                        continue;
                    }

                    if (map[g + 1, h] == 1)
                    {
                        count++;
                    }

                    if (map[g - 1, h] == 1)
                    {
                        count++;
                    }

                    if (map[g, h + 1] == 1)
                    {
                        count++;
                    }

                    if (map[g, h - 1] == 1)
                    {
                        count++;
                    }

                    if (map[g + 1, h + 1] == 1)
                    {
                        count++;
                    }

                    if (map[g - 1, h + 1] == 1)
                    {
                        count++;
                    }

                    if (map[g + 1, h - 1] == 1)
                    {
                        count++;
                    }

                    if (map[g - 1, h - +1] == 1)
                    {
                        count++;
                    }

                    if (count > 4)
                    {
                        map[g, h] = 1;
                    }
                }
            }
        }
    }

    private void randomEachSquare()
    {
        for (int g = 0; g < map.GetLength(0); g++)
        {
            for (int h = 0; h < map.GetLength(1); h++)
            {
                if (Random.Range(0f, 1f) > 0.75f)
                    map[g, h] = 1;
            }
        }
    }

    private void generateWalls()
    {
        for (int g = 0; g < map.GetLength(0); g++)
        {
            for (int h = 0; h < map.GetLength(1); h++)
            {
                if (g - 1 < 0 || g + 1 >= map.GetLength(0) || h - 1 < 0 || h + 1 >= map.GetLength(1))
                {
                    //catch out of bounds?
                    map[g, h] = 2;
                }
                else
                {
                    if (map[g, h] == 0)
                    {
                        if (map[g - 1, h] == 1 || map[g + 1, h] == 1 || map[g, h - 1] == 1 || map[g, h + 1] == 1)
                        {
                            map[g, h] = 2;
                        }
                    }
                }
            }
        }
    }
}
