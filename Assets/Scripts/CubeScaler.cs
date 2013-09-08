using UnityEngine;
using System.Collections;

public class CubeScaler : MonoBehaviour
{
    float initialScale;

    public float timeOffset;

    void Start ()
    {
        initialScale = transform.localScale.x;
    }

    void Update ()
    {
        transform.localScale = Vector3.one * (initialScale * (1.0f + Mathf.Sin (Time.time * 4.0f + timeOffset) * 0.2f));
    }
}
