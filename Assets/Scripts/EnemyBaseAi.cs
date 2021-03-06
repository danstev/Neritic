﻿using UnityEngine;
using System.Collections;

public class EnemyBaseAi : MonoBehaviour {

    //move movement type behaviour

    //1 melee
    //just move towards character when you have targett

    //2 caster
    //When not casting, maintain distance

    //3 rogue
    //Try keep distance, attack when player is not looking

    public int enemyType;
    private delegate void action();
    action enemyAI;

    public GameObject target;
    public float scanTimer;
    public float scanDist;

    public NPCStats stats;
    public float randomMoveTimerSet;
    private Vector3 randomMoveSpace;
    private float randomMoveMod = 0.0125f;
    private GameObject sprite;
    private Rigidbody r;
    private bool attacked = false;
    public bool alive = true;

    private float attackTimer = 0f;

    void Start () {
        r = GetComponent<Rigidbody>();
        
        if(enemyType == 1)
        {
            enemyAI += meleeEnemy;
        }
        else if(enemyType == 2)
        {
            enemyAI += mageEnemy;
        }
        else if (enemyType == 3)
        {
            enemyAI += rogueEnemy;
        }
        
        //enemyAI += moveBack;
    }

    void meleeEnemy()
    {
        if (target == null)
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

        if (target != null) //Add in other moves inside here
        {
            transform.LookAt(target.transform);
            //castMagic();
            aggressiveMove();
            //moveBack();
            //jumpForward();
        }
        else
        {
            randomMove();
        }
    }

    void mageEnemy()
    {
        if (target == null)
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

        if (target != null) //Add in other moves inside here
        {
            transform.LookAt(target.transform);
            castMagic();
            //moveBack();
        }
        else
        {
            randomMove();
        }

    }

    void rogueEnemy()
    {
        if (target == null)
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

        if (target != null) //Add in other moves inside here
        {
            transform.LookAt(target.transform);
            if(attacked)
            {
                moveBack();
            }
            else
            {
                aggressiveMove();
            }
        }
        else
        {
            randomMove();
        }
    }

    void Update () {
        if (alive)
        {
            enemyAI();
        }  
	}

    void moveBack()
    {
        if (randomMoveTimerSet <= 0)
        {
            randomMoveTimerSet = 3 * Random.Range(1, 2);
            float step = stats.momvementSpeedMod * Time.deltaTime;
            transform.position += transform.forward * -1 * step;
            attacked = false;
        }

        if (randomMoveTimerSet > 0)
        {
            float step = stats.momvementSpeedMod * Time.deltaTime;
            transform.position += transform.forward * -1 * step;
            randomMoveTimerSet -= Time.deltaTime;
        }
    }

    void jumpForward()
    {
        if (randomMoveTimerSet <= 0)
        {
            
            randomMoveTimerSet = 3 * Random.Range(1, 2);
            r.AddForce(new Vector3(transform.forward.x, 5, transform.forward.z), ForceMode.Impulse);
        }

        if (randomMoveTimerSet > 0)
        {
            randomMoveTimerSet -= Time.deltaTime;
        }
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
            attacked = true;
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


            aggressiveMove();

            if (attackTimer <= 0 && 25 > Vector3.Distance(transform.position, target.transform.position)) //stats.curMana >= stats.magicSpell.GetComponent<Spell>().manaCost && 
            {
                attackTimer = stats.magicTime;
                GameObject spell;
                spell = Instantiate(stats.magicSpell, transform.position + transform.forward * 1f, transform.rotation) as GameObject;
                Physics.IgnoreCollision(spell.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                Rigidbody spellR = spell.GetComponent<Rigidbody>();
                spellR.AddForce(spell.transform.forward * stats.magicSpeed);
                Spell spellA = spell.GetComponent<Spell>();
                spellA.setMagicAttack((int)stats.magicAttack);
                //Mana cost -
                //stats.curMana -= spellA.manaCost;
            }
            else if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
                float step = stats.momvementSpeedMod * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step / 2);
            }
        }

    }

