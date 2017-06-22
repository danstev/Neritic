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
        //print(xStartPos);
        //print(yStartPos);

        GameObject player;
        Statistics stats;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject g in players)
        {
            g.transform.position = new Vector3(xStartPos, 2, yStartPos);
            player = g;
            stats = g.GetComponent<Statistics>();
            GameObject rain = player.transform.Find("Rain").gameObject;
            Inventory i = player.GetComponent<Inventory>();
            PlayerControl cont = player.GetComponent<PlayerControl>();
            cont.startX = xStartPos;
            cont.startY = 2;
            cont.startZ = yStartPos;


            if (map == "dream")
            {
                stats.setMusic(1);
                GameObject sword = Instantiate(Resources.Load("Prefabs/Weapons/SmallSword")) as GameObject;
                i.AddItem(sword);
                //god stats etc
                stats.maxHealth = 500;
                stats.curHealth = 500;
                stats.maxMana = 250;
                stats.curMana = 250;
                stats.attack = 100;
                stats.magicAttack = 100;
                stats.strength = 20;
                stats.intellect = 20;
                stats.agility = 20;
                cont.refreshStats();
                stats.level = 10;
                i.updateAllStatisitics();
                rain.SetActive(false);

                

            }
            else if (map == "forest")
            {
                stats.setMusic(2);
                //check if sword is equipped
                if (i.equipped[0] == null)
                {
                    GameObject sword = Instantiate(Resources.Load("Prefabs/Weapons/SmallSword")) as GameObject;
                    i.AddItem(sword);
                }

                
                //rain on, drop all items
                stats.maxHealth = 100;
                stats.curHealth = 100;
                stats.maxMana = 50;
                stats.curMana = 50;
                stats.attack = 10;
                stats.magicAttack = 25;
                stats.strength = 5;
                stats.intellect = 5;
                stats.agility = 10;
                
                cont.refreshStats();
                i.updateAllStatisitics();
                stats.level = 1;
                rain.SetActive(true);

            }
            else if (map == "dungeon")
            {
                stats.setMusic(3);
                //rain off
                rain.SetActive(false);
            }
            else if (map == "endLevel")
            {
                stats.setMusic(4);
                //rain on
                rain.SetActive(true);
            }
        }

        GameObject e = GameObject.FindGameObjectWithTag("LevelSwitch");
        ExitLevel exit = e.GetComponent<ExitLevel>();
        exit.setLevel(map);

        
    }



	
}
