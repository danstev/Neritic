using UnityEngine;
using System.Collections;

public class TileTest : MonoBehaviour {

    Tile t;
	// Use this for initialization
	void Start () {
        t = GetComponent<Tile>();
        t.render();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
