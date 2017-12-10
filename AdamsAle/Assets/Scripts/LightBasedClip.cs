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

    string LI_Name = "";
    float LI_pr_Intensity;

	void Start () {
		
		AIcomp = GetComponent<AI> ();
		EntityCam = AIcomp.AICam;
		BaseFarClip = EntityCam.farClipPlane;	
		SuspFCI = AIcomp.SuspFarClipIncrease;
		DetcFCI = AIcomp.DetectedFarClipIncrease;
		AIAnimControl = GetComponentInChildren<Animator> ();
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

        if(LI_Name == LI.name)
        {
            if(LI_pr_Intensity != LI.Intensity)
            {
                AIcomp.player = LI.light;
                AIcomp.AIAnimControl.SetBool("Aware", true);
                AIcomp.KnownPos = LI.light.transform.position;
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("Player") != null)
                    AIcomp.player = GameObject.FindGameObjectWithTag("Player");
            }
        }

        LI_Name = LI.name;
        LI_pr_Intensity = LI.Intensity;
		
	}

	void OnTriggerEnter(Collider LightArea)
	{
		if(LightArea.CompareTag("LightArea"))
			LI = LightArea.GetComponent<LightIntensity> ();
	}

}
