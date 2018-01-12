using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float SwitchTime;

    private Collider Trigger;
    private MeshRenderer MRend;

    public LayerMask LMask;
    public LayerMask AlarmMask;

    public float AlarmRad;

    LineRenderer LineRend;

    public bool Vertical;

    private void Start()
    {
        Trigger = GetComponent<Collider>();
        MRend = GetComponent<MeshRenderer>();

        if (LineRend == null)
            LineRend = gameObject.AddComponent<LineRenderer>();

        LineRend.SetVertexCount(2);
        LineRend.SetWidth(0.25f,0.25f);

    }

    void Update ()
    {
        LineRend.SetPosition(0, transform.position);
        if (Time.time%(SwitchTime*2) >= SwitchTime)
        {
            RaycastHit _hit;
            if(Physics.Raycast(transform.position,-((Vertical/*Checks if movement is set to vertical*/) ? Vector3.right:Vector3.up),out _hit,LMask))
            {
                print(_hit.point);
                LineRend.SetPosition(1, _hit.point);
                if(_hit.transform.CompareTag("Player")) // If laser hits the player
                {
                    Collider[] HostileEnt = Physics.OverlapSphere(transform.position, AlarmRad, AlarmMask);// Alarm everyone around the laser
                    foreach(Collider Ent in HostileEnt)
                    {
                        HostileAI H_AI = Ent.GetComponent<HostileAI>();
                        if(H_AI != null)
                        {
                            H_AI.aware = true;
                            H_AI.distracted = true;
                            H_AI.distractionPos = _hit.point;
                        }
                    }
                }
            }
        }
        else
        {
            LineRend.SetPosition(1, transform.position);
        }
	}
}
