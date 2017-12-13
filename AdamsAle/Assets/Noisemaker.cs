using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisemaker : MonoBehaviour
{
    /*bool hit;
    public float Noisemaker_rad;
    public GameObject Effect;

    private void Update()
    {
        if(hit)
        {
            Collider[] Obj_inRad = Physics.OverlapSphere(transform.position, Noisemaker_rad);
            foreach(Collider Obj in Obj_inRad)
            {
                if (Obj.CompareTag("AI"))
                {
                    AI AIcomp = Obj.GetComponent<AI>();
                    if (!AIcomp.AIAnimControl.GetCurrentAnimatorStateInfo(0).IsTag("Detc"))
                    {
                        AIcomp.AIAnimControl.SetBool("Aware", true);
                        AIcomp.KnownPos = this.transform.position;
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (hit)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hit = true;
    }*/
}
