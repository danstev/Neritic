using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

    Inventory i;

    void Start()
    {
        i = GetComponent<Inventory>();
    }

    void worldUse()
    {
        //For item in inventory, detatch from parent, make real

        foreach (GameObject g in i.slots)
        {
            //Detatch from parent

            //activate the object

            //
        }

        //Destroy chest
        Destroy(this);
    }
}
