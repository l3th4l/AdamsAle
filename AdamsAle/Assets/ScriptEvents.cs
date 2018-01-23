using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEvents : MonoBehaviour
{
    [Header("Event properties")]
    public int EventIndex;

    public float EventTime = 0.0f;
    public bool StartEvent;
    public bool DestAfterEvent = false;

    private float StartTime = 0.0f;

    public GameObject[] Objects;

    public float[] FloatArray;

    void Update()
    {
        switch (EventIndex)
        {
            case 1:
                Activate();
                break;

            case 2:
                StartAfter();
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!StartEvent)
                StartTime = Time.time;
            StartEvent = true;
        }
    }

    void Activate()
    {
        if (StartEvent)
        {

            foreach (GameObject Ob in Objects)
            {
                Ob.SetActive(true);
            }
            if (DestAfterEvent)
                Destroy(this.gameObject);
        }
        else
        {
            foreach (GameObject Ob in Objects)
                Ob.SetActive(false);
        }
    }

    void StartAfter()
    {
        int index = 0;
        if (StartEvent)
        {
            if (EventTime - (Time.time - StartTime) > 0)
            {
                foreach (GameObject Ob in Objects)
                {
                    HostileAI HAI = Ob.GetComponent<HostileAI>();
                    if (HAI != null)
                    {
                        HAI.PassedTime = HAI.maxWalkTime;

                        if (HAI.detected || HAI.distracted)
                        {
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
            else
            {
                foreach (GameObject Ob in Objects)
                {
                    HostileAI HAI = Ob.GetComponent<HostileAI>();
                    if (HAI != null)
                    {
                        HAI.PassedTime = FloatArray[index];
                        index += 1;
                    }
                }
                if (DestAfterEvent)
                    Destroy(this.gameObject);
            }
        }
        else
        {
            foreach (GameObject Ob in Objects)
            {
                HostileAI HAI = Ob.GetComponent<HostileAI>();
                if (HAI != null)
                {
                    HAI.PassedTime = HAI.maxWalkTime;
                    if (HAI.detected || HAI.distracted)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
