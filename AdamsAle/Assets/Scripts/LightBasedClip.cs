using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]

public class LightBasedClip : MonoBehaviour {
	
	float BaseFarClip;
	public float IntensityFactor;
	public Camera EntityCam;
	LightIntensity LI;
	Animator AIAnimControl;
	float SuspFCI;
	float DetcFCI;
	AI AIcomp; 

	void Start () {
		
		AIcomp = GetComponent<AI> ();
		EntityCam = AIcomp.AICam;
		BaseFarClip = EntityCam.farClipPlane;	
		SuspFCI = AIcomp.SuspFarClipIncrease;
		DetcFCI = AIcomp.DetectedFarClipIncrease;
		AIAnimControl = GetComponent<Animator> ();
	}

	void Update () {

		
		if (EntityCam != null) {

		if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsTag ("Detc")) {
				EntityCam.farClipPlane = BaseFarClip +IntensityFactor * LI.Intensity + DetcFCI;
			
		}else {

				if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("SSP"))
					
					EntityCam.farClipPlane = BaseFarClip + IntensityFactor * LI.Intensity + SuspFCI;
				
				else
					
					EntityCam.farClipPlane = BaseFarClip + IntensityFactor * LI.Intensity;
			}
		}
		
	}

	void OnTriggerEnter(Collider LightArea)
	{
		if(LightArea.CompareTag("LightArea"))
			LI = LightArea.GetComponent<LightIntensity> ();
	}

}
