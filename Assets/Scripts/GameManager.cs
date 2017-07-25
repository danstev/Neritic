using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    string map;
    int level;
    float xStartPos;
    float yStartPos;
    string[] weatherEffects = new string[2] { "fog", "rain" };

    void Start()
    {
        Scene s = SceneManager.GetActiveScene();
        map = s.name;

        GameObject levelG = Resources.Load("Prefabs/Scripts/LevelGen") as GameObject;
        GameObject gen = Instantiate(levelG, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        LevelGen generateMap = gen.GetComponent<LevelGen>();

        GameObject f = new GameObject("Floors");
        generateMap.floors = f;
        GameObject c = new GameObject("Ceilings");
        generateMap.ceilings = c;
        GameObject w = new GameObject("Walls");
        generateMap.walls = w;

        generateMap.level = map;
        generateMap.spaceMod = UnityEngine.Random.Range(1.5f, 2f);

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
            Inventory i = player.GetComponent<Inventory>();
            PlayerControl cont = player.GetComponent<PlayerControl>();
            cont.startX = xStartPos;
            cont.startY = 2;
            cont.startZ = yStartPos;

            removeWeatherEffects(g);

            if (map == "dream")
            {
                
                stats.setMusic(1);
                GameObject sword = Instantiate(Resources.Load("Prefabs/Weapons/SmallSword")) as GameObject;
                i.addItem(sword);
                //god stats etc
                stats.maxHealth = 200;
                stats.curHealth = 200;
                stats.maxMana = 100;
                stats.curMana = 100;
                stats.attack = 30;
                stats.magicAttack = 30;
                stats.strength = 20;
                stats.intellect = 15;
                stats.agility = 15;
                cont.refreshStats();
                stats.level = 10;
                i.updateAllStatisitics();

                changeLightSize(player, 50,1);
                setParticles(Resources.Load("Prefabs/Particles/Fog") as GameObject, player);

            }
            else if (map == "forest")
            {
                stats.setMusic(2);
                //check if sword is equipped
                if (i.equipped[0] == null)
                {
                    GameObject sword = Instantiate(Resources.Load("Prefabs/Weapons/SmallSword")) as GameObject;
                    i.addItem(sword);
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

                changeLightSize(player, 30,1);
                setParticles(Resources.Load("Prefabs/Particles/Rain") as GameObject, g);

            }
            else if (map == "dungeon")
            {
                changeLightSize(player, 50, 1.5f);
                stats.setMusic(3);
                //rain off
            }
            else if (map == "endLevel")
            {
                changeLightSize(player, 30,1f);
                stats.setMusic(4);
                //rain on
                setParticles(Resources.Load("Prefabs/Particles/Rain") as GameObject, player);
            }
            else if( map == "WinterForest")
            {
                setParticles(Resources.Load("Prefabs/Particles/Fog") as GameObject, player);
            }
        }

        GameObject e = GameObject.FindGameObjectWithTag("LevelSwitch");
        ExitLevel exit = e.GetComponent<ExitLevel>();
        exit.setLevel(map);
    }

    void setParticles(GameObject g, GameObject player)
    {
        GameObject p = Instantiate(g);


        p.transform.SetParent(player.transform);
    }

    void removeWeatherEffects(GameObject player)
    {
        foreach(string s in weatherEffects)
        {
            try {
                GameObject g = player.transform.FindChild(s).gameObject;
                if (g != null)
                    Destroy(g);
            }
            catch(NullReferenceException e)
            {

            }
        }
    }

    void changeLightSize(GameObject p, float size, float intensity)
    {
        GameObject pointLight = p.transform.FindChild("Light").gameObject;
        Light l = pointLight.GetComponent<Light>();
        l.range = size;
        l.intensity = intensity;
    }



	
}
