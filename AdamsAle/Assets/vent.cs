using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vent : MonoBehaviour
{
    public bool vertical;
    Collider ventColl;

    Ladder lad;
    public float climbSpeed;
	void Start ()
    {
        ventColl = GetComponent<Collider>();
        if (vertical)
        {
            lad = gameObject.AddComponent<Ladder>();
            lad.ClimbSpeed = climbSpeed;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!vertical)
        {
            if (other.CompareTag("Player"))
                other.GetComponent<PlayerMovement>().crouching = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!vertical)
        {
            PlayerMovement mov = other.GetComponent<PlayerMovement>();
            if (other.CompareTag("Player"))
                if (mov.crouching)
                    mov.crouching = false;
        }
    }
}
