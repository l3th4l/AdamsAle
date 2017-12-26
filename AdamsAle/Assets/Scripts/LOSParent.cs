using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOSParent : MonoBehaviour
{
    Camera Cam;
    Light LOS;
	// Use this for initialization
	void Start ()
    {
        Cam = GetComponent<Camera>();
        LOS = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        LOS.spotAngle = Cam.fieldOfView;
        LOS.range = Cam.farClipPlane;
	}
}
