using UnityEngine;
using System.Collections;

public class TileTest : MonoBehaviour {

    Tile t;
    public GameObject tile;
	// Use this for initialization

	void Start () {
        t = GetComponent<Tile>();
        t.floorTile = tile;
        t.xPosition = 1;
        t.yPosition = 1;
        t.render();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
