using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualitySet : MonoBehaviour {

	// Use this for initialization
    
	void Start ()
    {
        if (QualitySettings.GetQualityLevel() == 0)
            this.GetComponent<Camera>().renderingPath = RenderingPath.Forward;
	}
}
