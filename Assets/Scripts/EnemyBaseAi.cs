using UnityEngine;
using System.Collections;

public class EnemyBaseAi : MonoBehaviour {

    //move movement type behaviour

    //1 melee
    //just move towards character when you have targett

    //2 caster
    //When not casting, maintain distance

    //3 rogue
    //Try keep distance, attack when player is not looking

    public GameObject target;
    public float scanTimer;
    public float scanDist;

    public Statistics stats;
    public float randomMoveTimerSet;
    private Vector3 randomMoveSpace;
    private float randomMoveMod = 0.0125f;
    private GameObject sprite;

    private float attackTimer = 0f;

    // Use this for initialization
    void Start () {
        //Transform s = transform.GetChild(0);
        //sprite = s.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        if(target == null)
        {
            if (scanTimer <= 0)
            {
                targetScan();
                scanTimer = Random.Range(0f, 5f);
            }
            else
            {
                scanTimer -= Time.deltaTime;

            }
        }

        if(target != null) //Add in other moves inside here
        {
            transform.LookAt(target.transform);
            //castMagic();
            //aggressiveMove();
            moveBack();
            //Back off move
            //Special Attack?
            //Jump
        }
        else
        {
            randomMove(); 
        }
	
	}

    void moveBack()
    {

        float step = stats.momvementSpeedMod * Time.deltaTime;
        transform.position += transform.forward * -1 * step;
    }

    void targetScan()
    {
        Collider[] detectColliders = Physics.OverlapSphere(transform.position, scanDist); //How efficient is this?
        for (int i = 0; i < detectColliders.Length; i++)
        {
            if (detectColliders[i].tag == "NPC" || detectColliders[i].tag == "Player")
            {
                target = detectColliders[i].gameObject;
                aggressiveMove();
            }
        }

        if (randomMoveTimerSet > 0)
        {
            transform.position += randomMoveSpace;
            randomMoveTimerSet -= Time.deltaTime;
        }
    }

    void aggressiveMove()
    {
        

        if (attackTimer <= 0 && stats.meleeReach > Vector3.Distance(transform.position, target.transform.position))
        {
           float[] v = new float[6];  
           v[0] = transform.eulerAngles.y;
           v[1] = stats.attack;
           v[2] = transform.position.x;
           v[3] = transform.position.y;
           v[4] = transform.position.z;
           target.transform.SendMessage(("takeDamage"), v, SendMessageOptions.DontRequireReceiver);
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

        if (scanDist < Vector3.Distance(transform.position, target.transform.position))
        {
            target = null;
        }
        
    }

    void randomMove()
    {
            if (randomMoveTimerSet <= 0)
            {
                randomMoveTimerSet = 3 * Random.Range(1, 2);
                randomMoveSpace = new Vector3(Random.Range(-1 * stats.momvementSpeedMod * randomMoveMod, 1 * stats.momvementSpeedMod * randomMoveMod), 0, Random.Range(-1 * stats.momvementSpeedMod * randomMoveMod, 1 * stats.momvementSpeedMod * randomMoveMod));
                transform.position += randomMoveSpace;
            }

            if (randomMoveTimerSet > 0)
            {
                transform.position += randomMoveSpace;
                randomMoveTimerSet -= Time.deltaTime;
            }
    }

    void castMagic()
    {
        transform.LookAt(target.transform);

        if (stats.magicSpell == null)
        {
            aggressiveMove();
        }
        else
        {
            if (stats.curMana >= stats.magicSpell.GetComponent<Spell>().manaCost && attackTimer <= 0)
            {
                attackTimer = stats.magicTime;
                GameObject spell;
                spell = Instantiate(stats.magicSpell, transform.position + transform.forward * 1, transform.rotation) as GameObject;
                Rigidbody spellR = spell.GetComponent<Rigidbody>();
                spellR.AddForce(spell.transform.forward * stats.magicSpeed);
                Spell spellA = spell.GetComponent<Spell>();
                spellA.setMagicAttack((int)stats.magicAttack);
                //Mana cost -
                stats.curMana -= spellA.manaCost;
            }
            else if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
                float step = stats.momvementSpeedMod * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step / 2);
            }
        }
    }
}
