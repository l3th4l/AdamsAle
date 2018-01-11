using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float SwitchTime;

    private Collider Trigger;
    private MeshRenderer MRend;

    public LayerMask LMask;

    LineRenderer LineRend;

    private void Start()
    {
        Trigger = GetComponent<Collider>();
        MRend = GetComponent<MeshRenderer>();

        try
        {
            LineRend = GetComponent<LineRenderer>();
        }
        catch(System.NullReferenceException)
        {
            Debug.Log("No line renderer assigned.");
            LineRend = new LineRenderer();
        }

        if (LineRend == null)
            LineRend = new LineRenderer();
        LineRend.SetPosition(0, transform.position);
        LineRend.SetWidth(0.5f, 0.5f);
    }

    void Update ()
    {
        if (Time.time%(SwitchTime*2) >= SwitchTime)
        {
            RaycastHit _hit;
            if(Physics.Raycast(transform.position,transform.up*-1,out _hit,LMask))
            {
                LineRend.SetPosition(1, _hit.point);
            }
        }
        else
        {
            LineRend.SetPosition(1, transform.position);
        }
	}
}
