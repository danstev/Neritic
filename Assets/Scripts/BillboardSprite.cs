using UnityEngine;
using System.Collections;

public class BillboardSprite : MonoBehaviour {

    private SpriteRenderer r;
    public Sprite front;
    public Sprite left;
    public Sprite right;
    public Sprite back;
    public bool rotate;

    void Start()
    {
        r = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 cameraPostion = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
        Vector2 parent = new Vector2(transform.forward.x, transform.forward.z);
        float enemyAngle = Vector2.Angle(cameraPostion, parent);
        Vector3 cross = Vector3.Cross(cameraPostion, parent);

        if(cross.z > 0)
        {
            enemyAngle = 360 - enemyAngle;
        }

        if (enemyAngle > 135 && enemyAngle < 225)
        {
            r.sprite = front;
        }
        else if(enemyAngle > 45 && enemyAngle < 135)
        {
            r.sprite = left;
        }
        else if(enemyAngle > 225 && enemyAngle < 315)
        {
            r.sprite = right;
        }
        else if (enemyAngle > 315 || enemyAngle < 45)
        {
            r.sprite = back;
        }
    }
}
