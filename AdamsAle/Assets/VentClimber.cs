using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentClimber : MonoBehaviour
{
    public bool entry;
    public bool zmov;
    public Transform TPoint;
    int mult;
    private void Start()
    {
        mult = (entry) ? 1 : -1;
    }
    bool CanCover;
    Collider other;
    private void Update()
    {
        if (CanCover)
        {
            if (entry)
            {
                if (!other.GetComponent<CharacterController>().isGrounded || zmov)
                    if (Input.GetAxisRaw("Vertical") * mult > 0 )
                    {
                        other.transform.position = TPoint.position;
                    }
            }
            else
            if (Input.GetAxisRaw("Vertical") * mult > 0 )
            {
                other.transform.position = TPoint.position;
            }
        }
    }
    private void OnTriggerStay(Collider Pl)
    {
        if (Pl.CompareTag("Player"))
        {
            CanCover = true;
            other = Pl;
        }else
        {
            CanCover = false;
        }
    }
    private void OnTriggerExit(Collider Pl)
    {
        if (Pl.CompareTag("Player"))
        {
            CanCover = false;
            other = Pl;
        }
    }
}
