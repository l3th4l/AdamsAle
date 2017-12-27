using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLock : MonoBehaviour
{
	void Update ()
    {
        Application.targetFrameRate = 120;
    }
}
