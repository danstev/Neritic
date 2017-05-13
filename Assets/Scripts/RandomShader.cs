using UnityEngine;
using System.Collections;

public class RandomShader : MonoBehaviour {

    public float min, max;
	void Start () {
        Renderer r = GetComponent<Renderer>();
        //print(r.material.GetFloat("Magnitude"));
        r.material.SetFloat("_Magnitude", Random.Range(min, max));
	}
	
}
