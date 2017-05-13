using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    string map;
    int level;
    float xStartPos;
    float yStartPos;

    void Start()
    {
        Scene s = SceneManager.GetActiveScene();
        map = s.name;

        GameObject levelG = Resources.Load("Prefabs/Scripts/LevelGen") as GameObject;
        GameObject gen = Instantiate(levelG, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        LevelGen generateMap = gen.GetComponent<LevelGen>();
        generateMap.level = map;
        generateMap.spaceMod = Random.Range(1.5f, 2f);
        generateMap.genMap();
        xStartPos = generateMap.getXStart();
        yStartPos = generateMap.getYStart();
        print(xStartPos);
        print(yStartPos);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject g in players)
        {
            g.transform.position = new Vector3(xStartPos, 2, yStartPos);
        }

        GameObject e = GameObject.FindGameObjectWithTag("LevelSwitch");
        ExitLevel exit = e.GetComponent<ExitLevel>();
        exit.setLevel(map);

        if(map == "dream")
        {
            //god stats etc
        }
        else if(map == "forest")
        {
            //basic stats no inv
        }
        else if(map == "dungeon")
        {
            //nothin?
        }
        else if(map == "endLevel")
        {
            //nothin?
        }
    }



	
}
