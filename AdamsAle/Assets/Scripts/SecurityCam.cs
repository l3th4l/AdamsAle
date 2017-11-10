using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    public float Tm;
    void Update () {
        Tm += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0.0f, Mathf.Clamp(Mathf.Sin(Tm) *120,-90.0f,90.0f) , 0.0f);
	}
}
