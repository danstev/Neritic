using UnityEngine;
using System.Collections;

public class EnemyDrop : MonoBehaviour {

    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;
    public GameObject rareDrop;
    public GameObject veryRareDrop;

    //Revamp using different objects

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        print("Item Drop");
        float rate = Random.Range(0f,1f);
        if(rate < 0.5f)
        {
            //Drop nothing
        }
        else if(rate < 0.95f)
        {
            //Drop a normal drop
            int item = Random.Range(0,3);
            if(item == 1)
            {
                Instantiate(drop1, transform.position, transform.localRotation);
            }
            else if(item == 2)
            {
                Instantiate(drop2, transform);
            }
            else if(item == 3)
            {
                Instantiate(drop3, transform);
            }
        }
        else if(rate > 0.95f)
        {
            //drop rare
            Instantiate(rareDrop, transform);
        }
        else if(rate > 0.98f)
        {
            //drop very rare
            Instantiate(veryRareDrop, transform);
        }
        else
        {
            //Drop nothing
        }
    }
}
