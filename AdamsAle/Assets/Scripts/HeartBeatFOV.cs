using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatFOV : MonoBehaviour {
    Camera cam;
    float fv;
    public float breathSpeed;
    public float FOV_Inc;
    float LTime;
    public float MaxTime;
	// Use this for initialization
	void Start ()
    {
        cam = GetComponent<Camera>();
        fv = cam.fieldOfView;
        LTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float CT = Time.time - LTime;
        cam.fieldOfView = Mathf.Sin(Time.time*breathSpeed)*FOV_Inc + fv + ((CT<= MaxTime)?CT:0.0f)*Input.GetAxis("Fire2")*4;
        if (Input.GetAxis("Fire2") == 0)
            LTime = Time.time;
	}
}
