using UnityEngine;
using System.Collections;

public class WinterForest {

    private int[,] map;
    int x1, x2, y1, y2;
    public int sx, sy, ex, ey;
    public int xHeight, yWidth;

    public int[,] genMap()
    {
        //WinterForest
        //Bunch of tress around edge, exit in middle, start around edge
        
        //Gen random trees, sparesly, 1/100?
        //Exit, entrance, rest is changing up trees a little, and adding tree foleys


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


}
