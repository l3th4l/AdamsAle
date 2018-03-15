using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour {

    public bool Locked;
    public bool Closed;
    public float closeTime;
    float pTime;

	void Start ()
    {
        pTime = 0;
	}
	
	void Update ()
    {
        pTime += Time.deltaTime;
        if (!Locked)
            if (Closed)
            {
                GetComponent<Collider>().enabled = true;
                pTime = 0;
            }
            else
            {
                GetComponent<Collider>().enabled = false;
                if (pTime > closeTime)
                    Closed = true;
            }
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
