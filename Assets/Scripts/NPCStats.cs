﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NPCStats : NetworkBehaviour{

    //Stats
    [SyncVar] public int curHealth;
    public int maxHealth;
    public int expGranted;
    public float attack;
    public AudioSource audioPlayer;
    public AudioClip hurtSound;
    public bool damageable = true;
    private Rigidbody r;
    public float momvementSpeedMod;
    public float meleeReach;
    public float attackTime = 1f;
    public float magicAttack;
    public float magicSpeed;
    public GameObject magicSpell;
    public float magicTime = 1f;

    //Enemy Drops
    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;
    public GameObject rareDrop;
    public GameObject veryRareDrop;

    private bool dead = false;
    private bool alive = true;

    // Use this for initialization
    void Start () {
        r = GetComponent<Rigidbody>();
}

    // Update is called once per frame
    void Update() {

        if (alive)
        {
            if (curHealth > maxHealth)
                curHealth = maxHealth;

            if (curHealth <= 0)
            {

                dead = true;
                alive = false;

                //turn off ai
                EnemyBaseAi ai = GetComponent<EnemyBaseAi>();
                ai.alive = false;

                //turn of anim
                Animator a = GetComponent<Animator>();
                a.enabled = false;

            }
        }

        if(dead == true)
        {
            KillNPC();
            dead = false;
        }
    }

    private void KillNPC()
    {
        SpriteRenderer sp = GetComponentInChildren<SpriteRenderer>();
        sp.FadeSprite(this, 2f, DestroySprite);
    }

    public void DestroySprite(SpriteRenderer renderer)
    {
        Collider[] detectColliders = Physics.OverlapSphere(transform.position, 25);
        for (int i = 0; i < detectColliders.Length; i++)
        {
            if (detectColliders[i].tag == "Player")
            {
                Statistics s = detectColliders[i].GetComponent<Statistics>();
                s.exp += expGranted;
                if (s.exp > s.expForLevel)
                {
                    s.levelUp(1);
                }
            }
        }

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
        Destroy(gameObject);
    }

    public void takeDamage(float[] dam)
    {


        if (damageable)
        {
            curHealth -= (int)dam[1];
            Vector3 p = new Vector3(dam[2], dam[3], dam[4]);
            p = transform.position - p;
            p = p.normalized;
            p.y = p.y + 1;
            audioPlayer.PlayOneShot(hurtSound);
            GameObject b = Resources.Load("Prefabs/Blood") as GameObject;
            r.AddForce(p * 125);

            for (int x = 0; x < Random.Range(1, 4); x++)
            {
                Vector3 v = gameObject.transform.position;
                Quaternion q = gameObject.transform.rotation;
                GameObject g = Instantiate(b, v, q) as GameObject;
                Rigidbody brdi = g.GetComponent<Rigidbody>();
                brdi.AddForce(p * 250);
            }

        }
    }

}

