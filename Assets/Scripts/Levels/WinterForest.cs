﻿using UnityEngine;
using System.Collections;

public class WinterForest {

    private int[,] map;
    int x1, x2, y1, y2;
    public int sx, sy, ex, ey;
    public int xHeight, yWidth;

    public int[,] genMap()
    {
        xHeight = map.GetLength(0);
        yWidth = map.GetLength(1);
        //WinterForest
        //Bunch of tress around edge, exit in middle, start around edge
        drawOutskirts(5);
        for(int x = 0; x < xHeight; x++)
        {
            for (int y = 0; y < yWidth; y++)
            {
                if (Random.Range(0.0f, 1f) > 0.5f)
                {
                    map[g, h] = 11; //Tree type, NYI!!
                }
            }
        }
        
        //Gen random trees, sparesly, 1/100?
        //Exit, entrance, rest is changing up trees a little, and adding tree foleys


        return map;
    }

    private void drawOutskirts(int thickness)
    {
        for (int g = 0; g < map.GetLength(0); g++)
        {
            for (int h = 0; h < thickness; h++)
            {
                if (Random.Range(0.0f, 1f) > 0.5f)
                {
                    map[g, h] = 0;
                }
            }
        }

        for (int g = 0; g < map.GetLength(0); g++)
        {
            for (int h = map.GetLength(1) - thickness; h < map.GetLength(1); h++)
            {
                if (Random.Range(0.0f, 1f) > 0.5f)
                {
                    map[g, h] = 0;
                }
            }
        }

        for (int g = map.GetLength(0) - thickness; g < map.GetLength(0); g++)
        {
            for (int h = 0; h < map.GetLength(1); h++)
            {
                if (Random.Range(0.0f, 1f) > 0.5f)
                {
                    map[g, h] = 0;
                }
            }
        }

        for (int g = 0; g < thickness; g++)
        {
            for (int h = 0; h < map.GetLength(1); h++)
            {
                if (Random.Range(0.0f, 1f) > 0.5f)
                {
                    map[g, h] = 0;
                }
            }
        }
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


}
