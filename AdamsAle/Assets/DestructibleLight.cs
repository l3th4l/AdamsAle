using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleLight : MonoBehaviour
{
    public LayerMask RayMask;
    Light Light_Comp;
    float Rad;
    float Intensity;

	// Use this for initialization
	void Start ()
    {
        Light_Comp = GetComponent<Light>();
        Rad = Light_Comp.range;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Intensity = Light_Comp.intensity;

        Collider[] EntInRad;
        EntInRad = Physics.OverlapSphere(transform.position, Rad);// Checks for AI Entitities in radius

        foreach(Collider Ent in EntInRad)
        {
            if(Ent.GetComponent<HostileAI>()!= null)
            {
                ViewAdjust VAEnt = Ent.transform.GetChild(0).GetComponentInChildren<ViewAdjust>();
                RaycastHit _hit;
                if(Physics.Raycast(transform.position,(Ent.transform.position-transform.position).normalized,out _hit,RayMask))// Checks if Entities are not obstructed
                {
                    if (_hit.collider == Ent)
                    {
                        float Increase = Intensity * (Rad - Vector3.Magnitude(Ent.transform.position - transform.position)) / Rad; ;

                        VAEnt.LI_Inc = Increase; // Increases the light intensity varible in the Entity's Camera's ViewAdjust script by the 1/distance from the light * intensity
                    }
                }
            }
        }
	}
}
