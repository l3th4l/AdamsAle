using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndParticle : MonoBehaviour
{
	void Update ()
    {
        if (GetComponent<ParticleSystem>().isStopped)
            Destroy(this.gameObject);
	}
}
