using UnityEngine;
using System.Collections;

public class TestLevel : MonoBehaviour {

    public string LevelToTest;

	// Use this for initialization
	void Start () {
        TestGenLevel();
	}

    void TestGenLevel()
    {
        WinterForest level = new WinterForest();
        int[,] map = new int[64, 64];
        level.setMap(map);
        map = level.genMap();
        GameObject tile = Resources.Load("Prefabs/Tiles/forestFloor") as GameObject;

        for(int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if(map[x,y] != 0)
                {
                    Vector3 mapPos = new Vector3(x, 0, y);
                    GameObject g = Instantiate(tile, mapPos, Quaternion.identity) as GameObject;
                    g.transform.rotation *= Quaternion.Euler(90, 0, 0);
                    g.isStatic = true;
                }
            }
        }
    }
	
}
