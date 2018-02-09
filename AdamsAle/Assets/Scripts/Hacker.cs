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

    public GameObject UIObj;// UI Object

    Hackable HObj;

    private GameObject CrossObj;
    private void Start()
    {
        CrossObj = GameObject.FindGameObjectWithTag("Crosshair");
    }
    void Update()
    {
        RaycastHit hitInf;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.z - Camera.main.transform.position.z;
        targetPos = (Camera.main.ScreenToWorldPoint(mousePos));
        targetPos.z = 0;

        if (Physics.Raycast(transform.position, (targetPos - transform.position).normalized, out hitInf, MaxRad, HackableMask))
        {
            HObj = hitInf.transform.GetComponent<Hackable>();
            if (Input.GetKey(HackKey))
            {
                HObj.HackProgression += HackSpeed * Time.deltaTime;
                HObj.transform.GetComponent<Hackable>().DispHack();

                //UI
                UIObj.SetActive(true);
                UIObj.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(HObj.transform.position);
                UIObj.GetComponent<HackUI>().Progression = Mathf.Clamp01(HObj.HackProgression / HObj.HackLimit);
                CrossObj.SetActive(false);
            }
            else
            {
                UIObj.SetActive(false);
                CrossObj.SetActive(true);
                if (HObj != null)
                    HObj.transform.GetComponent<Hackable>().HackProgression = 0;
            }
        }
        else
        {
            UIObj.SetActive(false);
            CrossObj.SetActive(true);
            if (HObj != null)
                HObj.transform.GetComponent<Hackable>().HackProgression = 0;
        }
    }
}
