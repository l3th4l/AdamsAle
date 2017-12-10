using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    public bool IsHacked;
    public float TurretRadius;

    //For shooting
    float nextTimeToFire = 0.0f;
    public float fireRate;
    public Transform weaponTransform;
    public float range = 100f;
    public float damage = 10f;

    [SerializeField]
    public LayerMask mask;


    [Space]
    [Header("Effects")]
    public float impactForce = 20f;
    public GameObject hitEffectPrefab;
    public ParticleSystem muzzleFlash;


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

            foreach (GameObject entity in Ent)
            {
                target = GameObject.FindGameObjectWithTag("Dummy").transform;
                if ((transform.position - entity.transform.position).sqrMagnitude < TurretRadius)
                {
                    print(entity.name);

                    RaycastHit _hit_ent;
                    if (Physics.Raycast(transform.position, (entity.transform.position - transform.position).normalized, out _hit_ent, range))
                    {
                        print(_hit_ent.transform.name);
                        if (_hit_ent.transform.gameObject.name == entity.name)
                        {
                            target = entity.transform;
                            print(entity.tag);
                            //Breaks the loop after shooting a single entity
                            break;
                        }
                    }
                }
            }
        }
        if ((transform.position - target.position).sqrMagnitude < TurretRadius)
        {
            //Look
            float angle = Mathf.Atan2((transform.position - target.position).y, (transform.position - target.position).x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.2f);

            //Shoot
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        
    }
    void Shoot()
    {
        //Debug.Log("Shoot" + nextTimeToFire);

        //Debug.Log("Shooting");

        //We are shooting, calling OnShoot method
        if (target != null)
        {
            OnShoot();

            RaycastHit _hit;
            Debug.Log("Hit created" + target.position);

            Debug.DrawRay(weaponTransform.position, (target.position - transform.position).normalized, Color.red);

            if (Physics.Raycast(weaponTransform.position, (target.position - transform.position).normalized, out _hit, range))
            {

                Debug.Log("Hit" + target.position);

                Enemy DMG_target = _hit.transform.GetComponent<Enemy>();
                if (DMG_target != null)
                {
                    DMG_target.TakeDamage(damage);
                }

                if (_hit.rigidbody != null)
                {
                    _hit.rigidbody.AddForce(-_hit.normal * impactForce);
                }

                //we hit something, calling onHit method
                OnHit(_hit.point, _hit.normal);
            }
        }
    }

    void OnHit(Vector3 _hitPos, Vector3 _normal)
    {

        //Hit Effect
        GameObject hitEffect = (GameObject)Instantiate(hitEffectPrefab, _hitPos, Quaternion.LookRotation(_normal));
        Destroy(hitEffect, 2f);
    }

    void OnShoot()
    {
        // We are shooting, subtracting ammo
        //currentAmmo--;

        //Shoot Effect
        muzzleFlash.Play();
    }
}
