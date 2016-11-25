﻿using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour {

    public bool damageable = false;

    public string nameOfMob;
    public int health;
    public int mana;
    public int armour;
    public int attack;
    public float meleeReach;
    public float attackTime = 1f;
    public int magicAttack;
    public float momvementSpeedMod = 1f;

    public Inventory inv;
    public GameObject[] equipment = new GameObject[10];

    public int level;
    public int exp;
    private int expLeft;
    public int expGranted;

    //Get component
    private Rigidbody r;

    //Main hand
    //Offhand
    //Helmet
    //Chestpiece
    //Leggings
    //Gloves
    //Feet



	// Use this for initialization
	void Start () {
        r = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if( health <= 0 )
        {
            Destroy(gameObject);
        }
	
	}

    public void takeDamage(float[] dam)
    {
        if(damageable)
        health -= (int)dam[1];
        Vector3 p = new Vector3(dam[2], dam[3], dam[4]);
        p = transform.position - p;
        p = p.normalized;
        p.y = p.y + 1;
        r.AddForce (p * 250);
    }

}
