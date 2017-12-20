using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAdjust : MonoBehaviour
{
    public int ViewType;
    float base_FCP;
    float base_FOV;
    public float LI_Inc;
    public float LI_Factor;
    
	void Start ()
    {
        base_FCP = GetComponent<Camera>().farClipPlane;
        base_FOV = GetComponent<Camera>().fieldOfView;
    }
	
	void Update ()
    {
        switch (ViewType)
        {
            case 1: // Default
                GetComponent<Camera>().farClipPlane = base_FCP+LI_Inc*LI_Factor;
                GetComponent<Camera>().fieldOfView = base_FOV;
                break;

            case 2: // Detected
                GetComponent<Camera>().farClipPlane = base_FCP*3;
                GetComponent<Camera>().fieldOfView = base_FOV/4;
                break;

            case 3: // Distracted
                GetComponent<Camera>().farClipPlane = base_FCP + LI_Inc * LI_Factor;
                GetComponent<Camera>().fieldOfView = base_FOV/1.5f;
                break;

            case 4: // Suspicious
                GetComponent<Camera>().farClipPlane = base_FCP * 1.25f + LI_Inc * LI_Factor;
                GetComponent<Camera>().fieldOfView = base_FOV / 2f;
                break;


        }
    }
}
