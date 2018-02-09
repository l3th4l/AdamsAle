using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    public float HackSpeed;
    public float MaxRad;
    Vector3 targetPos;    
    public KeyCode HackKey;
    public LayerMask HackableMask;
    void Update()
    {
        RaycastHit hitInf;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.z - Camera.main.transform.position.z;
        targetPos = (Camera.main.ScreenToWorldPoint(mousePos));
        targetPos.z = 0;

        if (Physics.Raycast(transform.position, (targetPos - transform.position).normalized, out hitInf, MaxRad, HackableMask))
        {
            if (Input.GetKey(HackKey))
            {
                hitInf.transform.GetComponent<Hackable>().HackProgression += HackSpeed * Time.deltaTime;
                hitInf.transform.GetComponent<Hackable>().DispHack();
            }
            else

                hitInf.transform.GetComponent<Hackable>().HackProgression = 0;
        }
    }
}
