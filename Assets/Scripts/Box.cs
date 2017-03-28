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
        float yPos = 0;
        foreach (GameObject g in i.slots)
        {
            //Detatch from parent
            g.transform.parent = null;
            //activate the object
            g.SetActive(true);
            g.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + yPos,this.transform.position.z);
            yPos += 0.25f;
        }

        //Destroy chest
        Destroy(this);
    }
}
