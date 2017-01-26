using UnityEngine;
using System.Collections;

public class EnemyBaseAi : MonoBehaviour {

    //target stuff
    public GameObject target;
    public float scanTimer;
    public float scanDist;


    //move stuff
    public Statistics stats;
    public float randomMoveTimerSet;
    private Vector3 randomMoveSpace;

    //Attack stuff
    private float attackTimer = 0f;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(target == null)
        {
            targetScan();
        }

        if(target != null)
        {
            aggressiveMove();
        }
        else
        {
            //randomMove(); //until this is fixed
        }
	
	}

    void targetScan()
    {
        Collider[] detectColliders = Physics.OverlapSphere(transform.position, scanDist); //How efficient is this?
        for (int i = 0; i < detectColliders.Length; i++)
        {
            //print(detectColliders[i].tag);
            if (detectColliders[i].tag == "NPC" || detectColliders[i].tag == "Player")
            {
                target = detectColliders[i].gameObject;
                aggressiveMove();
            }
        }

    }

    void aggressiveMove()
    {
        if (attackTimer <= 0 && stats.meleeReach > Vector3.Distance(transform.position, target.transform.position))
        {
            print(stats.attack);
            //Do attack
           float[] v = new float[6];
                
           v[0] = transform.eulerAngles.y;
           v[1] = stats.attack;
           v[2] = transform.position.x;
           v[3] = transform.position.y;
           v[4] = transform.position.z;
           target.transform.SendMessage(("takeDamage"), v, SendMessageOptions.DontRequireReceiver);


           //Set timer to attackspeed
           attackTimer = stats.attackTime;
        }
        else if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            float step = stats.momvementSpeedMod * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step / 2);

        }
        else
        {
            float step = stats.momvementSpeedMod * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }

        /*
        //If target moves away
        if (scanDist * 2 > Vector3.Distance(transform.position, target.transform.position)) //Sets target to null as this is ran???
        {
            target = null;
        }
        */

    }

    void randomMove()
    {
            if (randomMoveTimerSet <= 0)
            {
                randomMoveTimerSet = 3 * Random.Range(1, 2);
                randomMoveSpace = new Vector3(Random.Range(-1 * stats.momvementSpeedMod, 1 * stats.momvementSpeedMod), 0, Random.Range(-1 * stats.momvementSpeedMod, 1 * stats.momvementSpeedMod));
                transform.position += randomMoveSpace;
            }

            if (randomMoveTimerSet > 0)
            {
                transform.position += randomMoveSpace;
                randomMoveTimerSet -= Time.deltaTime;
            }
        

    }
}
