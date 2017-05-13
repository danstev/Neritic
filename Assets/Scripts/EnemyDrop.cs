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
        //Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        Vector3 pos = transform.position;
        float rate = Random.Range(0f,1f);
        if (rate < 0.95f)
        {
            
            //Drop a normal drop
            int item = Random.Range(0,4);
            if(item == 1)
            {
                print("should drop1");
                GameObject d = Instantiate(drop1, transform.position, Quaternion.identity) as GameObject;
                d.transform.position = pos;
            }
            else if(item == 2)
            {
                print("should drop2");
                Instantiate(drop2, transform.position, Quaternion.identity);
            }
            else if(item == 3)
            {
                print("should drop3");
                Instantiate(drop3, transform.position, Quaternion.identity);
            }
        }
        else if(rate > 0.95f)
        {
            //drop rare
            Instantiate(rareDrop, pos, Quaternion.identity);
        }
        else if(rate > 0.98f)
        {
            //drop very rare
            Instantiate(veryRareDrop, pos, Quaternion.identity);
        }
        else
        {
            //Drop nothing
        }
        print("done");
    }
}
