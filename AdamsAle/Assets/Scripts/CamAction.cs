using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAction : MonoBehaviour {
    Plane[] CamPl;
    Collider PlayerCol;
    SecurityCam AttachedMovement;

	void Start ()
    {
        PlayerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        AttachedMovement = GetComponentInParent<SecurityCam>();
	}

	void Update ()
    {
        print(Time.fixedDeltaTime);
        CamPl = GeometryUtility.CalculateFrustumPlanes(this.GetComponent<Camera>());
        if(GeometryUtility.TestPlanesAABB(CamPl,PlayerCol.bounds))
        {
            AttachedMovement.enabled = false;
            AttachedMovement.Tm = (Mathf.Sin(AttachedMovement.Tm) * 90 > 0) ? 90 : 180;
        }
        else
        {
            AttachedMovement.enabled = true;
        }
	}
}
