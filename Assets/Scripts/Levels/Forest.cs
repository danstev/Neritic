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

        return map;
    }

    public void setMap(int[,] m)
    {
        map = m;
    }
}
