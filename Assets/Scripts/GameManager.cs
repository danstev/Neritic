using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    string level;
    float xStartPos;
    float yStartPos;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject g in players)
        {
            g.transform.position = new Vector3(xStartPos, 1, yStartPos);
        }
    }


	
}
