using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDoor : MonoBehaviour
{
    Collider DoorCol;
    eventAct Activator;

    private void Start()
    {
        DoorCol = GetComponent<Collider>();
        Activator = GetComponent<eventAct>();
    }
    void Update ()
    {
        if (Activator.active)
            DoorCol.isTrigger = true;
        else
            DoorCol.isTrigger = false;
	}
}
