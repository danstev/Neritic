using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{

    void Update() 
	{
        transform.LookAt( new Vector3 (Camera.main.transform.position.x, 0.75f, Camera.main.transform.position.z), Vector3.up);
    }
}
