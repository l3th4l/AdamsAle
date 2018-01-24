using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    CharacterController controller;
    float RunTime = 0.0f;
    [SerializeField]
    float interval;

    [SerializeField]
    GameObject Ripple;

    float GroundHeight;
    float CurrentHeight;

    void Start ()
    {
        controller = GetComponent<CharacterController>();
        RaycastHit R_hit;
        if(Physics.Raycast(transform.position,-transform.up,out R_hit))
        {
            GroundHeight = (transform.position - R_hit.point).y;
        }
	}
	
	void FixedUpdate ()
    {
        if (GetComponent<PlayerMovement>().sprinting)// if running
        {
            RunTime += Time.fixedDeltaTime;
            Debug.Log(CurrentHeight + "" + GroundHeight);
            if (Mathf.RoundToInt(RunTime * 100) % interval == 0)// If stepping 
            {

                RaycastHit R_hit;
                if (Physics.Raycast(transform.position, -transform.up, out R_hit))
                    CurrentHeight = (transform.position - R_hit.point).y;

                //if (CurrentHeight <= GroundHeight)
                //{
                    Instantiate(Ripple, transform.position + transform.right, Ripple.transform.rotation);
                    Collider[] Entities = Physics.OverlapSphere(transform.position, Ripple.transform.localScale.x);
                    foreach (Collider Ent in Entities)
                    {
                        HostileAI AI = Ent.GetComponent<HostileAI>();
                        if (AI != null)
                        {
                            if (!AI.inSearch)
                            {
                                AI.distractionPos = transform.position;
                                AI.distracted = true;
                            }
                            else
                            {
                                AI.knownPos = transform.position + transform.up * 0.75f;
                            }
                        }
                    }
                //}
            }
        }
        else
            RunTime = 0.0f;
	}
}
