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

        //Draw boundaries
        /*for (int g = 0; g < map.GetLength(0); g++)
        {
            for (int h = 0; h < thickness; h++)
            {
                map[g, h] = 10; //10 is tree to the drawer.
            }
        }*/

        /*for (int g = 0; g < map.GetLength(0); g++)
        {
            for (int h = map.GetLength(1) - thickness; h < map.GetLength(1); h++)
            {
                map[g, h] = 10;
            }
        }*/

        /*for (int g = map.GetLength(0) - thickness; g < map.GetLength(0); g++)
        {
            for (int h = 0; h < map.GetLength(1); h++)
            {
                map[g, h] = 10;
            }
        }*/

        for (int g = map.GetLength(0) - thickness; g < map.GetLength(0); g++)
        {
            for (int h = map.GetLength(1) - thickness; h < map.GetLength(1); h++)
            {
                map[g, h] = 10;
            }
        }


        return map;
    }

    public void setMap(int[,] m)
    {
        map = m;
    }
}
