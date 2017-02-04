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
    public string statNeeded;
    public int statNeed;
    public string description;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void worldUse(Inventory inv)
    {
        //print("asdsad");
        inv.AddItem(gameObject);
    }

}
