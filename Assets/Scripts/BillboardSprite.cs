using UnityEngine;
using System.Collections;

public class BillboardSprite : MonoBehaviour {

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
        float enemyAngle = Vector2.Angle(new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z), new Vector2(transform.forward.x, transform.forward.z));
        //Only returns from 0 - 180, so don't use if you are wanting to use different sprite for sides;
        //fuck this won't work for me;

        if(enemyAngle < 60)
        {
            r.sprite = front;
        }
        else if(enemyAngle > 60 && enemyAngle < 120)
        {
            r.sprite = side;
        }
        else if(enemyAngle > 120)
        {
            r.sprite = back;
        }

    }
}
