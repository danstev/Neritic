using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Disable : NetworkBehaviour{

	// Use this for initialization
	void Start () {

        if( !isLocalPlayer )
        {
            gameObject.SetActive(false);
        }
	
	}
	

}
