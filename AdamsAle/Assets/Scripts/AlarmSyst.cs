using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class AlarmSyst : MonoBehaviour
{
    public bool hacked;
    public LayerMask AlarmMask;
    SphereCollider SphereCol;
    float alarmRadius;

    public bool alarmed;

    private void Start()
    {
        SphereCol = GetComponent<SphereCollider>();
        alarmRadius = SphereCol.radius;
    }
    private void Update()
    {
        if (alarmed)
        {
            Collider[] Entities;
            Entities = Physics.OverlapSphere(transform.position, alarmRadius, AlarmMask);
            foreach (Collider col in Entities)
            {
                HostileAI EntAI = col.GetComponent<HostileAI>();
                EntAI.aware = true;
                if (EntAI.distractionPos != transform.position)
                {
                    EntAI.distracted = true;
                    EntAI.distractionPos = this.transform.position;
                }
            }
        }
    }

}
