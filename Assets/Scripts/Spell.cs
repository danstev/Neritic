using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    //on start get stats of what damage etc to dooo
    //On collision do damage plz

    public int magicAttack;
    public int manaCost;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        //print(col.gameObject.name);
        if(col.gameObject.name == "Player") //check for friendly/seethrough etc? here
        {
            float[] v = new float[6];
            v[1] = magicAttack;
            v[2] = transform.position.x;
            v[3] = transform.position.y;
            v[4] = transform.position.z;
            col.transform.SendMessage(("takeDamage"), v, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
        else
        {
            float[] v = new float[6];
            v[1] = magicAttack;
            v[2] = transform.position.x;
            v[3] = transform.position.y;
            v[4] = transform.position.z;
            col.transform.SendMessage(("takeDamage"), v, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }

    public void setMagicAttack(int magicA)
    {
        magicAttack = magicA;
    }

}
