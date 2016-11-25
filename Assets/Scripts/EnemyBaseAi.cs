using UnityEngine;
using System.Collections;

public class EnemyBaseAi : MonoBehaviour {

    //target stuff
    public GameObject target;
    public float scanTimer;
    public float scanDist;


    //move stuff
    public float moveSpeed;
    public float attackDistance;
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
            randomMove();
        }
	
	}

    void targetScan()
    {
        Collider[] detectColliders = Physics.OverlapSphere(transform.position, scanDist);
        for (int i = 0; i < detectColliders.Length; i++)
        {
            //print(detectColliders[i].tag);
            if (detectColliders[i].tag == "NPC" || detectColliders[i].tag == "Player")
            {
                target = detectColliders[i].gameObject;
                print(target.name);
                aggressiveMove();
            }
        }

    }

    void aggressiveMove()
    {
        if (stats.attackTime <= 0 && stats.meleeReach > Vector3.Distance(transform.position, target.transform.position))
        {
            //Do attack
            RaycastHit melee = new RaycastHit();
            if (Physics.Raycast(transform.position, transform.forward, out melee, stats.meleeReach))
            {
                Debug.DrawLine(transform.position, melee.transform.position, Color.cyan, 10f);
                float[] v = new float[6];
                v[0] = transform.eulerAngles.y;
                v[1] = stats.attack;
                v[2] = transform.position.x;
                v[3] = transform.position.y;
                v[4] = transform.position.z;
                melee.transform.SendMessage(("takeDamage"), v, SendMessageOptions.DontRequireReceiver);
            }

            //Set timer to attackspeed
            attackTimer = stats.attackTime;
        }
        else if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step / 2);

        }
        else
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }

        //If target moves away
        if (target != null && scanDist * 2 > Vector3.Distance(transform.position, target.transform.position))
        {
            target = null;
        }

    }

    void randomMove()
    {
            if (randomMoveTimerSet <= 0)
            {
                randomMoveTimerSet = 3 * Random.Range(1, 2);
                randomMoveSpace = new Vector3(Random.Range(-1 * moveSpeed, 1 * moveSpeed), 0, Random.Range(-1 * moveSpeed, 1 * moveSpeed));
                transform.position += randomMoveSpace;
            }

            if (randomMoveTimerSet > 0)
            {
                transform.position += randomMoveSpace;
                randomMoveTimerSet -= Time.deltaTime;
            }
        

    }
}
