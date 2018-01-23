using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleLight : MonoBehaviour
{
    public float LightRad;
    public float Angle;

    public float visInt;

    private void Awake()
    {
        Angle = GetComponent<Light>().spotAngle;
    }

    public Vector3 DirToAngle(float angle , bool Global)
    {
        if (!Global)
            angle += transform.eulerAngles.x;

        return (new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)));
    }
}
