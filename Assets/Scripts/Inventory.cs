using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public GameObject[] slots = new GameObject[20];
    public GameObject[] equipped = new GameObject[10];
    public GameObject[] spells = new GameObject[50];
	
	//Main hand
    //Offhand
    //Helmet
    //Chestpiece
    //Leggings
    //Gloves
    //Feet
    //Neck
    //ring
    //ring

    private int amountOfItems = 0;
    private int lastSlot = 0;

    public float totalWeight;
    public int gold;

    public void AddItem ( GameObject i)
    {
        Item item = i.GetComponent<Item>();

        //If equippable
        if(item.equipable == true)
        {
            if(item.type == "equipment")
            {
                Equipment e = i.GetComponent<Equipment>();
                string slot = e.slot;
                //check if you have one equipped, if not, equip it CAN TURN OFF IN SETTINGS?
                //if not equipped, go to the other part
            }
            else if(item.type == "spell")
            {
                //ADD TO SPELL LIST, NO AUTO EQUIP

            }
        }
        else //Not equippable
        {
            for (int x = 0; x < amountOfItems; x++)
            {
                Item currentSlot = slots[x].GetComponent<Item>();

                if (item.ID == currentSlot.ID) //If you have one in inventory
                {
                    if (currentSlot.unique) //Check unique
                    {
                        //Display ui error here?
                        return;
                    }
                    else if (currentSlot.stackable)//check stackable
                    {
                        int spaceLeft = currentSlot.amount - currentSlot.held; //Amount of space left in currently held stack
                        if (spaceLeft < item.held) //if you have more than space left
                        {
                            print("More than space left");
                            //fill up stack then make new one

                            int toAdd = currentSlot.amount - currentSlot.held; //Too add onto current stack
                            item.held = item.held - toAdd; //to new slot

                            currentSlot.held += toAdd;

                            slots[lastSlot] = i;
                            totalWeight += item.weight * item.held;
                            i.transform.parent = transform;
                            i.SetActive(false);

                            lastSlot++;
                            //Audio cue here?
                            return;
                        }
                        else if (spaceLeft == 0) //If no space left
                        {
                            print("no space left");
                            //Add to next free slot
                            slots[lastSlot] = i;
                            totalWeight += item.weight * item.held;
                            i.transform.parent = transform;
                            i.SetActive(false);

                            lastSlot++;
                            //Audio cue here?
                            return;

                        }
                        else //you have space left
                        {
                            print("space left");
                            //Add to current itemstack
                            Item increaseVal = slots[x].GetComponent<Item>();
                            increaseVal.held += item.held;
                            Destroy(i);
                            //Audio cue here?
                            return;
                        }
                    }
                }
            }

            if (amountOfItems < 20 && !item.unique) //Not in inv, and not unique
            {
                slots[lastSlot] = i;
                totalWeight += item.weight * item.held;
                i.transform.parent = transform;
                i.SetActive(false);

                lastSlot++;
                amountOfItems++;
                return;
            }
            else if (amountOfItems == 20)// inv full
            {
                //inv full error
                return;
            }

            //not full, not unique, none in inv
            //Add to next free slot
            slots[lastSlot] = i;
            totalWeight += item.weight;
            i.transform.parent = transform;
            i.SetActive(false);

            lastSlot++;
            //Audio cue here?
            return;
        }
       
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void recalcWeight() //Should not be needed if i did add properly, but is nice for when dropping maybe etc, maybe just remove any weight stuff for other actions, just plonk this after each?
    {
        int weight = 0;
        foreach(GameObject i in slots)
        {
            Item w = i.GetComponent<Item>();
            weight += w.weight * w.held;
        }

        foreach (GameObject i in equipped)
        {
            Item w = i.GetComponent<Item>();
            weight += w.weight * w.held;
        }

        totalWeight = weight;
    }

    void updateStatisitics()
    {
        int s = 0;
        int a = 0;
        int i = 0;

        int wDam = 0;
        int sDam = 0; 

        foreach(GameObject g in equipped)
        {
            Equipment e = g.GetComponent<Equipment>();
            s += e.strength;
            a += e.agility;
            i += e.intellect;

            if(e.weapon == true)
            {
                
            }
        }

    }

    void updateInv()
    {

    }

    void updateEquipment()
    {

    }

    void updateSpells()
    {

    }

    void updateStatsPage()
    {

    }
}
