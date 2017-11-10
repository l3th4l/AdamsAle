using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    public float ClimbSpeed;
    private Collider other;
    private bool inTrig;

    private void Start()
    {
        other = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
    }

    private void FixedUpdate()
    {

        if (inTrig)
        {
            if (other.gameObject.CompareTag("Player"))
                other.attachedRigidbody.velocity = new Vector3(other.attachedRigidbody.velocity.x, 0.0f, other.attachedRigidbody.velocity.z) + transform.up * Input.GetAxisRaw("Vertical") * ClimbSpeed;
        }

    }
    private void OnTriggerEnter(Collider pl)
    {
        if(pl.CompareTag("Player"))
            inTrig = true;
    }
    private void OnTriggerExit(Collider pl)
    {
        if (pl.CompareTag("Player"))
            inTrig = false;
    }
}
