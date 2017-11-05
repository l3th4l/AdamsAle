using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    public bool IsHacked;
    public float TurretRadius;
	void Start ()
    {
        IsHacked = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update ()
    {
        if(IsHacked)
        {
            print("Hacked");
            GameObject[] Ent= GameObject.FindGameObjectsWithTag("AI");
            foreach(GameObject entity in Ent)
            {
                if ((transform.position - entity.transform.position).sqrMagnitude < TurretRadius)
                {
                    target = entity.transform;
                    print(entity.tag);
                    break;
                }
            }
        }
        if ((transform.position - target.position).sqrMagnitude < TurretRadius)
        {
            float angle = Mathf.Atan2((transform.position - target.position).y, (transform.position - target.position).x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.2f);
        }
        
    }
}
