using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    string map;
    int level;
    float xStartPos;
    float yStartPos;

    void Start()
    {
        GameObject level = GameObject.FindGameObjectWithTag("levelGen");
        LevelGen gen = level.GetComponent<LevelGen>();
        xStartPos = gen.getXStart();
        yStartPos = gen.getYStart();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject g in players)
        {
            g.transform.position = new Vector3(xStartPos, 2, yStartPos);
        }
    }



	
}
