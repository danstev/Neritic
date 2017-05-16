using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    //TODO
    //Update all pages
    //Remove item function
    
    public GameObject[] slots = new GameObject[20];
    public GameObject[] equipped = new GameObject[10];
    public GameObject[] spells = new GameObject[50];

    Quaternion weaponRotation;
	
	//Main hand     0
    //Offhand       1
    //Helmet        2
    //Chestpiece    3
    //Leggings      4
    //Gloves        5
    //Feet          6
    //Neck          7
    //ring          8
    //ring          9

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
            
            if (item.type == "equipment")
            {
                Equipment e = i.GetComponent<Equipment>();
                int slot = e.slot;
                if( equipped[slot] == null )
                {
                    equipped[slot] = i;
                    //totalWeight += item.weight * item.held;
                    
                    if (slot == 0) //Weapon slot, need another for 2nd weapon slot?
                    {
                        item.equipWeapon();
                        PlayerControl p = GetComponent<PlayerControl>();
                        p.refreshWeapon(i);
                        CapsuleCollider c = i.GetComponent<CapsuleCollider>();
                        c.enabled = false;
                    }
                    else
                    {
                        item.equip();
                        i.SetActive(false);
                    }
                    e.equip();
                    i.transform.parent = transform;
                    updateAllStatisitics();
                    updateGUITextures();
                    return;
                }
                else
                {
                    //Add into next free slot
                    slots[lastSlot] = i;
                    //totalWeight += item.weight * item.held;
                    i.transform.parent = transform;
                    i.SetActive(false);
                    lastSlot++;
                    updateGUITextures();
                    return;

                }
                //check if you have one equipped, if not, equip it CAN TURN OFF IN SETTINGS?
                //if not equipped, go to the other part
            }
            else if(item.type == "spell")
            {
                //ADD TO SPELL LIST, NO AUTO EQUIP
                updateGUITextures();
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
                            //totalWeight += item.weight * item.held;
                            i.transform.parent = transform;
                            i.SetActive(false);
                            updateGUITextures();

                            lastSlot++;
                            //Audio cue here?
                            return;
                        }
                        else if (spaceLeft == 0) //If no space left
                        {
                            print("no space left");
                            //Add to next free slot
                            slots[lastSlot] = i;
                            //totalWeight += item.weight * item.held;
                            i.transform.parent = transform;
                            i.SetActive(false);
                            updateGUITextures();

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
                            updateGUITextures();
                            //Audio cue here?
                            return;
                        }
                    }
                }
            }

            if (amountOfItems < 20) //inv not full
            {
                slots[lastSlot] = i;
                //totalWeight += item.weight * item.held;
                i.transform.parent = transform;
                i.SetActive(false);

                lastSlot++;
                amountOfItems++;
                updateGUITextures();
                return;
            }
            else if (amountOfItems == 20)// inv full
            {
                //inv full error
                return;
            }

            slots[lastSlot] = i;
            //totalWeight += item.weight;
            i.transform.parent = transform;
            i.SetActive(false);

            lastSlot++;
            updateGUITextures();
            return;
        }
       
    }

    void addItem()
    {
        //equipable
            //is slot full?
                //yes, inv
                //no, equip
        //item
            //check if you alreeady have some
                //if yes, check held is less than amount
                //if no add to inv with held 1
            
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

    public void updateAllStatisitics()
    {
        int s = 0;
        int a = 0;
        int i = 0;

        int wDam = 0;
        int sDam = 0;

        Statistics stats = GetComponent<Statistics>();
        s += stats.strength;
        a += stats.agility;
        i += stats.intellect;

        sDam += (int)stats.magicAttack;


        foreach(GameObject g in equipped)
        {
            if (g == null)
            {

            }
            else
            {
                Equipment e = g.GetComponent<Equipment>();
                s += e.strength;
                a += e.agility;
                i += e.intellect;

                if (e.weapon == true)
                {
                    wDam = e.attack;
                }
                //Same for spell
            }

        }

        //set these on the stats page
        updateStatsPage(s,a,i,wDam,sDam);
        PlayerControl p = GetComponent<PlayerControl>();
        p.refreshStats();

    }

    public void dropItem(GameObject item)
    {
        Item i = item.GetComponent<Item>();
        totalWeight -= i.weight;
        item.transform.parent = null;
        i.equipped = false;
        item.transform.position = item.transform.position + Vector3.forward;
        item.SetActive(true);

        GameObject g = GameObject.Find("OutPortal").gameObject;
        item.transform.parent = g.transform;
        updateGUITextures();
    }

    public void dropEverything()
    {
        foreach(GameObject i in slots)
        {

        }

        foreach(GameObject e in equipped)
        {

        }
    }

    public void equipEquipment(GameObject toEquip)
    {
        Item item = toEquip.GetComponent<Item>();
        Equipment e = toEquip.GetComponent<Equipment>();

        if (equipped[e.slot] == null)
        {
            item.equip();
            toEquip.SetActive(false);
            e.equip();
            toEquip.transform.parent = transform;
            updateAllStatisitics();
            updateGUITextures();
        }
        else
        {
            unequipItem(equipped[e.slot]);
            item.equip();
            toEquip.SetActive(false);
            e.equip();
            toEquip.transform.parent = transform;
            updateAllStatisitics();
            updateGUITextures();
        }
}

    public void deleteItem(int itemId) //used for using items
    {
        //find item in slots
        //if amount is > 1, -1, else destroy;
        Item target;
        GameObject g;
        foreach(GameObject i in slots)
        {
            Item item = i.GetComponent<Item>();
            if(item.ID == itemId)
            {
                target = item;
                g = i;
                if (target.held > 1)
                {
                    target.held -= 1;
                }
                else
                {
                    Destroy(g);
                }
                updateGUITextures();
                return;
            }
        }
    }

    void dropAllItems()
    {
        foreach(GameObject i in slots)
        {
            dropItem(i);
        }

        foreach (GameObject i in equipped)
        {
            dropItem(i);
        }
        updateAllStatisitics();
    }

    void updateStatsPage(int strength, int agility, int intellect, int wDam, int sDam)
    {
        Statistics s = GetComponent<Statistics>();
        s.actualstrength = strength;
        s.actualIntellect = intellect;
        s.actualAgility = agility;
        s.attack = wDam;
        s.magicAttack = sDam;
        //Should be all stats
    }

    void updateGUITextures()
    {
        PlayerControl p = GetComponent<PlayerControl>();
        for(int x = 0; x < slots.Length -1; x++)
        {
            if(slots[x] != null)
            {
                SpriteRenderer s = slots[x].GetComponent<SpriteRenderer>();
                p.invTextures[x] = s.sprite.texture;
            }
        }

        for (int x = 0; x < equipped.Length; x++)
        {
            if (equipped[x] != null)
            {
                SpriteRenderer s = equipped[x].GetComponent<SpriteRenderer>();
                p.equipTextures[x] = s.sprite.texture;
            }
        }
    }

    public bool weaponEquippedCheck()
    {
        if (equipped[1] == null)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    public void unequipItem(GameObject toUnequip)
    {
        Equipment i = toUnequip.GetComponent<Equipment>();
        int slot = i.slot;
        //if(slot == 0)
        //{
            i.unEquip();
            equipped[slot] = null;
            toUnequip.SetActive(true);
            toUnequip.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
            toUnequip.transform.parent = null;
        CapsuleCollider c = toUnequip.GetComponent<CapsuleCollider>();
        c.enabled = true;
        updateAllStatisitics();
        updateGUITextures();
       // }
        //else
        //{
            
        //}

    }
}
