﻿
using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public int ID;
    public string itemName;
    public int held;
    public int amount;
    public string type;
    public bool stackable;
    public bool unique;
    public int weight;
    public bool equipable;
    public bool equipped = false;
    public string statNeeded;
    public int statNeed;
    public string description;

    public int health;
    public int mana;
    public int strength;
    public int agility;
    public int intellect;

    // Use this for initialization
    void Start () {

        if(!equipped && equipable)
        {
            Animator a = GetComponent<Animator>();
            a.enabled = false;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void worldUse(Inventory inv)
    {
        inv.AddItem(gameObject);
    }

    public void equip()
    {
        Animator a = GetComponent<Animator>();
        a.enabled = true;
        equipped = true;
    }

    public void equipWeapon()
    {
        Animator a = GetComponent<Animator>();
        a.enabled = true;
        equipped = true;
    }

    public void unequip()
    {
        Animator a = GetComponent<Animator>();
        a.enabled = false;
        equipped = false;
    }

    public void use(Statistics s, Inventory i)
    {
        if(equipable)
        {
            i.equipEquipment(gameObject);
        }
        else
        {
            s.curHealth += health;
            s.curMana += mana;
            s.strength += strength;
            s.agility += agility;
            s.intellect += intellect;
            if(held == 1)
            {
                Destroy(gameObject);
            }
            else
            {
                held--;
            }
        }
    }

}
