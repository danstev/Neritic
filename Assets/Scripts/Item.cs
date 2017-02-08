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

    void equip()
    {
        Animator a = GetComponent<Animator>();
        a.enabled = true;
    }

}
