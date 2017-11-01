using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DisableUnseenFlare : MonoBehaviour {
	GameObject MainCam;
	LensFlare LFlare;
	// Use this for initialization
	void Start () 
	{
		MainCam = GameObject.FindGameObjectWithTag ("Main Camera");
		LFlare = GetComponent<LensFlare> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		RaycastHit Obj;
		if (Physics.Raycast (MainCam.transform.position, transform.position, out Obj)) {
			if (Obj.collider.gameObject == this.gameObject)
				LFlare.enabled = true;
			else
				LFlare.enabled = false;
		}
	}
}
