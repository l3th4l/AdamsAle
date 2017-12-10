using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine : MonoBehaviour
{
    [Header("Parameters")]
    GameObject BlastRad_obj;
    float BlastRad;

    public GameObject explosion_particle;
    public Transform world;

    public float Damage;

    public bool Blast;
    public bool Blast_all = false;
    public bool Hacked = false;

    [Space]
    [Header("Mine network")]

    public GameObject[] M_Net;

    // Use this for initialization
    void Start ()
    {
        Blast = false;
        BlastRad_obj = this.transform.GetChild(0).gameObject;
        BlastRad = BlastRad_obj.GetComponent<SphereCollider>().radius;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(Hacked)
        {
            foreach (GameObject M in M_Net)
                M.GetComponent<mine>().Hacked = true;
        }

		if(Blast || Blast_all)
        {
            Collider[] BlastEnt = Physics.OverlapSphere(transform.position, BlastRad);
            foreach(Collider Ent in BlastEnt)
            {
                if (Ent.GetComponent<Enemy>() != null)
                {
                    Ent.GetComponent<Enemy>().TakeDamage(Damage);
                }
                
                Rigidbody Ent_rb = Ent.attachedRigidbody;
                if (Ent_rb != null)
                    Ent_rb.AddExplosionForce(Damage * 10, transform.position, BlastRad);
            }
            if(Blast_all)
            {
                foreach (GameObject M in M_Net)
                    M.GetComponent<mine>().Blast = true;
            }
            Destroy(this.gameObject);
            GameObject.Instantiate(explosion_particle,transform.position,transform.rotation,world);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        print(other.tag + other.CompareTag("Player") );
        if (Hacked)
        {
            if (other.CompareTag("AI"))
                Blast = true;
        }
        else
        {
            if (other.CompareTag("Player"))
                Blast = true;
        }
    }
}
