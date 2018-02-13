using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour {

    public bool Locked;
    public bool Closed;

	void Start () {
		
	}
	
	void Update ()
    {
        if (!Locked)
            if (Closed)
                GetComponent<Collider>().enabled = true;
            else
                GetComponent<Collider>().enabled = false;
        else
        {
            gameObject.AddComponent<Hackable>();
            if (GetComponent<Hackable>().Hacked)
            {
                Locked = false;
                Destroy(GetComponent<Hackable>());
            }
        }

    }
}
