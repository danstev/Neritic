using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

    //Just holding info for now????

    public int slot;
    public bool weapon;
    public int attack;
    public int armour;
    public int baseStrength;
    public int baseAgility;
    public int baseIntellect;
    public int strength;
    public int agility;
    public int intellect;
    public int level;

    public void Start()
    {
        print("sdsad");
        int s = Random.Range(0,3);
        if(s <= 1)
        {
            strength = baseStrength + Random.Range(level * 1, level * 3);
            agility = baseAgility;
            intellect = baseIntellect;
        }
        else if(s <= 2)
        {
            agility = baseAgility + Random.Range(level * 1, level * 3);
            intellect = baseIntellect;
            strength = baseStrength;

        }
        else if(s <= 3)
        {
            intellect = baseIntellect + Random.Range(level * 1, level * 3);
            agility = baseAgility;
            strength = baseStrength;
        }
    }

    public void equip()
    {
        if(slot == 1 || slot == 2)
        {
            //move to correct location on body.
            transform.position = new Vector3(0.43f, 0f, 0.53f);
            transform.rotation = getRot();
            Animator a = GetComponent<Animator>();
            a.enabled = true;
        }
        else
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }

    private Quaternion getRot()
    {
        Quaternion r;
        Item[] i = gameObject.GetComponentsInChildren<Item>();
        foreach (Item items in i)
        {
            if (items.equipped && items.tag == "Weapon")
            {
                r = items.transform.localRotation;
                return r;
            }
        }
        return new Quaternion(0,0,0,0);
        
    }



}
