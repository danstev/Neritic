using UnityEngine;
using System.Collections;

public class Dungeon {

    private int[,] map;
    int x1, x2, y1, y2;

    public int[,] genMap()
    {
        int x = map.GetLength(0);
        int y = map.GetLength(1);
        int rooms = Random.Range(8,12);
        //Make starting room near edge.
        buildStartRoom();
        buildEndRoom();

        
        x1 = 0;
        x2 = 0;

        for (int i = 0; i < rooms; i++)
        {
            int height = Random.Range(4, 12);
            int width = Random.Range(4, 12);
            int offsetH = Random.Range(0, x - height - 1);
            int offsetW = Random.Range(0, y - width - 1);

            for (int q = offsetH; q <= height + offsetH; q++)
            {
                for (int w = offsetW; w <= width + offsetW; w++)
                {
                    map[q, w] = 1;

                    if(w == width + offsetW - 1)
                    {
                        
                        x1 = q;
                        x2 = w;
                        if(y1 == -1)
                        {
                            y1 = x1;
                            y2 = x2;
                        }
                        else
                        {
                            buildCorridor(x1,x2,y1,y2,1);
                            y1 = x1;
                            y2 = x2;
                        }
                    }
                }
            }

        }
        buildCorridor(x1, x2, map.GetLength(0) - 8 + 3, map.GetLength(0) - 8 + 3, 1);

        buildWalls();

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
                map[i + c, t + c] = 1;
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
            map[h, x2] = tileType;
        }

        for (int h = middle; h < y1; h++)
        {
            map[h, y2] = tileType;
        }

        if(x2 < y2)
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
        

    }

    private void buildWalls()
    {
        int[,] tempMap = map;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int t = 0; t < map.GetLength(1); t++) //For every box
            {
                if(checkBorders(i,t))
                {
                    tempMap[i, t] = 2;
                }
            }
        }
        map = tempMap;
    }
    
    private bool checkBorders(int x, int y) //true if wall should be here
    {
        bool check = false;
        
        if( oobCheck(x, y) )
        {
            return check;
        }

        if (map[x - 1, y] != 0 && map[x - 1, y] != 2)
        {
            check = true;
            //Perhaps do wall type here as well?
        }

        if (map[x + 1, y] != 0 && map[x + 1, y] != 2)
        {
            check = true;
        }

        if (map[x, y + 1] != 0 && map[x, y + 1] != 2)
        {
            check = true;
        }

        if (map[x, y - 1] != 0 && map[x, y - 1] != 2)
        {
            check = true;
        }

        return check;
    }

    private bool oobCheck(int x, int y) //shouldn't need this maybe im bad
    {
        bool check = false; //False = no oob, true == oob

        if (x == 0 || x == map.GetLength(0))
        {
            return true;
        }

        if (y == 0 || y == map.GetLength(0))
        {
            return true;
        }

        return check;
    }

}
