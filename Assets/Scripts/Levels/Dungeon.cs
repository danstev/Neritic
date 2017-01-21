using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

    private int[,] map;

    public int[,] genMap()
    {
        int x = map.GetLength(0);
        int y = map.GetLength(1);
        int rooms = (x * y) / 30;
        //Generate rooms
        for(int i = 0; i < rooms; i++)
        {
            int offsetH = Random.Range(0, x - 4);
            int offsetW = Random.Range(0, y - 4);
            int height = Random.Range(4, 12);
            int width = Random.Range(4, 12);
            for(int q = offsetH; q < height + offsetH; q++)
            {
                for(int w = offsetW; w < width + offsetW; w++)
                {
                    map[q, w] = 1;
                }
            }
        }

        //Generate corridors

        //Generate other stuff
        return map;
    }

    public void setMap(int [,] m)
    {
        map = m;
    }


}
