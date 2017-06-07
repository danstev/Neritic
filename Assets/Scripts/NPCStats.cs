using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NPCStats : NetworkBehaviour{

    //Stats
    [SyncVar] public int curHealth;
    public int maxHealth;
    //Enemy Drops
    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;
    public GameObject rareDrop;
    public GameObject veryRareDrop;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update() {

        if (curHealth <= 0)
        {

            if (tag == "Enemy")
            {
                float rate = Random.Range(0f, 1f);
                if (rate > 0.75f)
                {
                    int item = Random.Range(0, 4);
                    if (item == 1)
                    {
                        Instantiate(drop1, transform.position, Quaternion.identity);
                    }
                    else if (item == 2)
                    {
                        Instantiate(drop2, transform.position, Quaternion.identity);
                    }
                    else if (item == 3)
                    {
                        Instantiate(drop3, transform.position, Quaternion.identity);
                    }
                }
                else if (rate > 0.95f)
                {
                    Instantiate(rareDrop, transform.position, Quaternion.identity);
                }
                else if (rate > 0.98f)
                {
                    Instantiate(veryRareDrop, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
    }

}

