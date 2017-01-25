using UnityEngine;
using System.Collections;

public class Bleed : MonoBehaviour {

    private float f;
	// Use this for initialization
	void Start () {
        f = Random.Range(5f, 15f);
	
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, f);
	}
}
