using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    GameObject target;

    public void SetTarget (GameObject target)
    {
        this.target = target;
    }

    void Update ()
    {
        if (target != null) {
            transform.LookAt (target.transform.position);
        }
    }
}
