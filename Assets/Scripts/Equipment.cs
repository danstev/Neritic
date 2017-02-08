using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

    //Just holding info for now????

    public int slot;
    public bool weapon;
    public int attack;
    public int armour;
    public int strength;
    public int agility;
    public int intellect;

    public void equip()
    {
        if(slot == 1 || slot == 2)
        {
            //move to correct location on body.
        }
        else
        {
            //set parent + move to body
        }
    }

}
