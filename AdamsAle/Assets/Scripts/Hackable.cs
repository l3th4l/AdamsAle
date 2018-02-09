using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackable : MonoBehaviour
{
    public float HackLimit;
    public float HackProgression;

    [HideInInspector]
    public bool Hacked;

	void Update ()
    {
        if (HackProgression >= HackLimit)
            Hacked = true;
        MatSwitch();
	}

    public Material DefaultMat;
    public Material HackingMat;
    public Material HackedMat;
    void MatSwitch()
    {
        if (!Hacked)
        {
            if (HackProgression > 0)
                GetComponent<MeshRenderer>().material = HackingMat;
            else
                GetComponent<MeshRenderer>().material = DefaultMat;
        }
        else
        {
            HackProgression = HackLimit;
            GetComponent<MeshRenderer>().material = HackedMat;
        }
    }

    public void DispHack()
    {
        Debug.Log(HackProgression);
    }
}
