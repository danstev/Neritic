using UnityEngine;
using System.Collections;

public class DisableMoving : MonoBehaviour {


    void OnColliderEnter(Collider col)
    {
        col.gameObject.SetActive(true);
    }

    void OnColliderLeave(Collider col)
    {
        col.gameObject.SetActive(false);
    }
}
