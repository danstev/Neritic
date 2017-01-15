using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

	// Use this for initialization

    //on start get stats of what damage etc to dooo
    //On collision do damage plz

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        print(col.gameObject.name);
        Destroy(this);
    }

}
