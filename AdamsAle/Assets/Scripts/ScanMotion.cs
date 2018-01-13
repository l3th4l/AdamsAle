using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanMotion : MonoBehaviour
{
    public float Distance;
    public float M_Time;

    public float Offset;

    public bool Vertical;
    
    private float Velocity;
    private float PassedTime;

    Vector3 InitPos;

	// Use this for initialization
	void Start ()
    {
        Velocity = (Distance+Offset) / M_Time;
        if (Offset != 0)
            PassedTime = Velocity / Offset;
        else
            PassedTime = 0;

        InitPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(((Vertical/*Checks if movement is set to vertical*/)?transform.up:transform.right) * (((Time.time + PassedTime) % M_Time*2  >= M_Time) ? Velocity : -Velocity)*Time.deltaTime);

    }
}
