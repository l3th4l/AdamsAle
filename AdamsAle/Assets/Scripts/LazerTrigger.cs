using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTrigger : MonoBehaviour {
    public bool playerInTrig;

    private void OnTriggerEnter(Collider other)
    {
        playerInTrig = other.CompareTag("Player");
    }
    private void OnTriggerExit(Collider other)
    {
        playerInTrig = !other.CompareTag("Player");
    }
}
