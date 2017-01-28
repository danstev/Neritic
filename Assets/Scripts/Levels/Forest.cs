using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

    //LArge map sent in, outside is blocked.
    //Fill outside with trees
    //Large depth, but random it a little, and make it smooth.
    //Sparse/dense setting

    private int[,] map;

    public int[,] genMap()
    {
        int x = map.GetLength(0);
        int y = map.GetLength(1);
        float density = 0.05f;
        int thickness = (x + y) / 20; //100 + 100 = 200 / 20 = 10

        if(thickness < 7)
        {
            thickness = 7;
        }

        //Draw boundaries
        for (int g = 0; g < map.GetLength(0); g++)
        {
            for (int h = 0; h < thickness; h++)
            {
                if (Random.Range(0.0f, 1f) > 0.5f)
                {
                    map[g, h] = 10; //10 is tree to the drawer.
                }
            }
        }

        for (int g = 0; g < map.GetLength(0); g++)
        {
            for (int h = map.GetLength(1) - thickness; h < map.GetLength(1); h++)
            {
                if (Random.Range(0.0f, 1f) > 0.5f)
                {
                    map[g, h] = 10; //10 is tree to the drawer.
                }
            }
        }

        for (int g = map.GetLength(0) - thickness; g < map.GetLength(0); g++)
        {
            for (int h = 0; h < map.GetLength(1); h++)
            {
                if (Random.Range(0.0f, 1f) > 0.5f)
                {
                    map[g, h] = 10; //10 is tree to the drawer.
                }
            }
        }

        for (int g = 0;  g < thickness; g++)
        {
            for (int h = 0; h < map.GetLength(1); h++)
            {
                if (Random.Range(0.0f, 1f) > 0.5f)
                {
                    map[g, h] = 10; //10 is tree to the drawer.
                }
            }
        }

        for(int i = 0; i < 4; i++)
        {
            for (int g = 0; g < x; g++)
            {
                for (int h = 0; h < y; h++)
                {
                    int count = 0;
                    if (g - 1 < 0 || g + 1 >= map.GetLength(0) || h - 1 < 0 || h + 1 >= map.GetLength(1))
                    {
                        map[g, h] = 10;
                        continue;
                    }

                    if( map[g + 1, h] == 10)
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

                    if (map[g - 1, h-+ 1] == 10)
                    {
                        count++;
                    }

                    if(count > 4)
                    {
                        map[g, h] = 10;
                    }
                    else
                    {
                        if(Random.Range(0f, 1f) < density)
                        {
                            map[g, h] = 10;
                        }
                    }

                }
            }
        }

        return map;
    }

    public void setMap(int[,] m)
    {
        map = m;
    }
}
