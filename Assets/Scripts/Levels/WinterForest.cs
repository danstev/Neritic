using UnityEngine;
using System.Collections;

public class WinterForest {

    private int[,] map;
    int x1, x2, y1, y2;
    public int sx, sy, ex, ey;
    public int xHeight, yWidth;

    public int[,] genMap()
    {
        //CAVE 2
        //Draw a bunch of rooms
        //keep a hold of middle of rooms
        //Smooth eeverything
        //Draw corridors between each room in order, then some not in order
        //

        xHeight = map.GetLength(0);
        yWidth = map.GetLength(1);
        int rooms = Random.Range(18, 24);

        x1 = 0;
        x2 = 0;

        //Do start room here

        for (int i = 0; i < rooms; i++)
        {
            int height = Random.Range(4, 12);
            int width = Random.Range(4, 12);
            int offsetH = Random.Range(0, xHeight - height - 1);
            int offsetW = Random.Range(0, yWidth - width - 1);

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
        smoothMap(2);
        //Last room build
        buildCorridor(x1, x2, map.GetLength(0) - 8 + 3, map.GetLength(1) - 8 + 3, 1);
        //Build a bunch of random pillars
        for (int r = 0; r < 50; r++)
        {
            int q = Random.Range(1, xHeight - 2);
            int z = Random.Range(1, yWidth - 2);

            map[q, z] = 0;
            map[q, z + 1] = 0;
            map[q + 1, z] = 0;
            map[q, z - 1] = 0;
            map[q - 1, z] = 0;
        }


        buildEndRoom();
        buildCorridor(x1, x2, y1, y2, 1);
        buildCorridor(4, 4, xHeight - 5, yWidth - 5, 1);
        return map;
    }


    void buildStartRoom()
    {
        for (int x = 1; x < 6; x++)
        {
            for (int i = 1; i < 6; i++)
            {
                if (x == 4 && i == 4)
                {
                    sx = x;
                    sy = i;
                    map[x, i] = 2;
                }
                else
                {
                    map[x, i] = 1;
                    y1 = x;
                    y2 = i;
                }
            }
        }
    }

    void buildEndRoom()
    {
        for (int x = xHeight - 10; x < xHeight - 3; x++)
        {
            for (int i = yWidth - 10; i < yWidth - 3; i++)
            {
                if (x == xHeight - 7 && i == yWidth - 7)
                {
                    ex = x;
                    ey = i;
                    map[x, i] = 3;
                }
                else
                {
                    map[x, i] = 1;
                    y1 = x;
                    y2 = i;
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
