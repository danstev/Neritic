using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
    private SpriteRenderer r;
    public Sprite front;
    public Sprite side;
    public Sprite back;
    public bool rotate;

    void Start()
    {
        r = GetComponent<SpriteRenderer>();
    }

    void Update() 
	{
        transform.LookAt( new Vector3 (Camera.main.transform.position.x, 0.5f, Camera.main.transform.position.z), Vector3.up);



    }
}
