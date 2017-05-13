using UnityEngine;
using System.Collections;

public class Forest {

    //LArge map sent in, outside is blocked.
    //Fill outside with trees
    //Large depth, but random it a little, and make it smooth.
    //Sparse/dense setting

    private int[,] map;
    public int sx, sy, ex, ey;

    public int[,] genMap()
    {
        int x = map.GetLength(0);
        int y = map.GetLength(1);
        fillMapWithGroundFirst();
        float density = 0.05f;
        int thickness = (x + y) / 20;

        if(thickness < 7)
        {
            thickness = 7;
        }
        //Draw boundaries
        drawOutskirts(thickness);
        smoothMap(4, density);

        drawStart();
        drawEnd();
        cleanUp();
        return map;
    }

    void cleanUp()
    {
        for(int x = 0 + 1; x < map.GetLength(0) - 1; x ++)
        {
            for (int y = 0 + 1; y < map.GetLength(1) - 1; y++)
            {
                if(map[x,y] == 1)
                {
                    if(map[x -1,y] != 1 && map[x + 1, y] != 1 && map[x, y - 1] != 1 && map[x, y] + 1  != 1)
                    {
                        map[x, y] = 0;
                    }
                }
            }
        }
    }


    void drawStart()
    {
        for(int i = 4; i < 13; i++)
        {
            for(int x = 4; x < 13; x++)
            {
                if(i == 10 && x == 10)
                {
                    map[i, x] = 2;
                    sx = i;
                    sy = x;
                }
                else
                {
                    map[i, x] = 1;
                }
            }
        }
    }

    public void setMap(int[,] m)
    {
        map = m;
    }

    private void drawEnd()
    {
        int wid = map.GetLength(0) /2;
        int hei = map.GetLength(1) /2;
        for (int i = 0; i < 5; i++)
        {
            for(int x = 0; x < 5; x++)
            {
                if (i == 3 && x == 3)
                {
                    map[wid + i, hei + x] = 3;
                }
                else
                {
                    map[wid + i, hei + x] = 1;
                }
            }
        }
    }

    private void fillMapWithGroundFirst()
    {
        for(int i = 0; i < map.GetLength(0); i++)
        {
            for(int x = 0; x < map.GetLength(1); x++)
            {
                map[i, x] = 1;
            }
        }
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

    private void smoothMap(int smoothness, float density)
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
                        map[g, h] = 0;
                        continue;
                    }

                    if (map[g + 1, h] == 10)
                    {
                        count++;
                    }

                    if (map[g - 1, h] == 10)
                    {
                        count++;
                    }

                    if (map[g, h + 1] == 10)
                    {
                        count++;
                    }

                    if (map[g, h - 1] == 10)
                    {
                        count++;
                    }

                    if (map[g + 1, h + 1] == 10)
                    {
                        count++;
                    }

                    if (map[g - 1, h + 1] == 10)
                    {
                        count++;
                    }

                    if (map[g + 1, h - 1] == 10)
                    {
                        count++;
                    }

                    if (map[g - 1, h - +1] == 10)
                    {
                        count++;
                    }

                    if (count > 4)
                    {
                        map[g, h] = 0;
                    }
                    else
                    {
                        if (Random.Range(0f, 1f) < density)
                        {
                            map[g, h] = 0;
                        }
                    }

                }
            }
        }
    }
}
