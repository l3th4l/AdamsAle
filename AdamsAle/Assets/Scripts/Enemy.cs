using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public float health = 50f;

    Light LightComp;

    bool Dead = false;

    [SerializeField]
    Material DestroyedMat;

    private void Start()
    {
        LightComp = GetComponent<Light>();
    }

    public void TakeDamage(float _amount)
    {
        health -= _amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (LightComp == null)
            Destroy(gameObject);
        else
        {
            LightComp.intensity = 0.0f;
            if(!Dead)// Alerts enemies
            {
                GetComponent<MeshRenderer>().material = DestroyedMat;
                Collider[] Entities = Physics.OverlapSphere(transform.position, LightComp.range);
                foreach (Collider Ent in Entities)
                {
                    HostileAI AI = Ent.GetComponent<HostileAI>();
                    if (AI != null)
                    {
                        if (!AI.inSearch)
                        {
                            AI.distractionPos = transform.position;
                            AI.distracted = true;
                        }
                        else
                        {
                            AI.knownPos = transform.position + transform.up * 0.75f;
                        }
                    }
                }
            }
            Dead = true;
        }
    }


}
