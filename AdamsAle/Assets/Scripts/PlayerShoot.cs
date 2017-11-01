using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    [SerializeField]
    [Header("Weapon Info")]
    private Transform weaponTransform;
    public float range = 100f;
    public float damage = 10f;
    public float fireRate;
    public float inaccuracy;
    private float nextTimeToFire = 0f;
    private Vector3 randomOffset;
    [SerializeField]
    private Animator weaponAnimator;
    [SerializeField]
    private LayerMask mask;

    /*
    [Space]
    [Header("Reload")]
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;
    */

    [Space]
    [Header("Effects")]
    public float impactForce = 20f;
    public GameObject hitEffectPrefab;
    public ParticleSystem muzzleFlash;


    private bool isShooting = false;

    private void Start()
    {
       // currentAmmo = maxAmmo;

        if (weaponTransform == null)
        {
            Debug.LogError("PlayerShoot: No weaponTransform referenced! :')");
            this.enabled = false;
        }
    }

    private void Update()
    {
        //if (isReloading)
        //    return;

        //if(currentAmmo<= 0)
        //{
        //    StartCoroutine(Reload());
        //}
        float rand = Random.Range(0.0f, 0.5f);
        float a =  rand - (rand*Input.GetAxis("Fire2"));
        randomOffset = new Vector3(0.0f, a , 0.0f)*inaccuracy;

        Debug.DrawRay(transform.position, (weaponTransform.right + randomOffset).normalized, Color.red);

        if(Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
        
        if(Input.GetMouseButton(0))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

        if(isShooting)
        {
            GetComponent<PlayerController>().enabled = false;
        }
        else
        {
            GetComponent<PlayerController>().enabled = true;
        }

       // if(Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
       //{
       //     StartCoroutine(Reload());
       // }
    }

    void Shoot()
    {

        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Shoot");
        //Debug.Log("Shooting");

        //We are shooting, calling OnShoot method
        OnShoot();

        RaycastHit _hit;
        if (Physics.Raycast(weaponTransform.position, (weaponTransform.right + randomOffset).normalized, out _hit, range, mask))
        {

            //Debug.Log("We hit " + _hit.transform.name);
            Enemy target = _hit.transform.GetComponent<Enemy>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }

            if(_hit.rigidbody != null)
            {
                _hit.rigidbody.AddForce(-_hit.normal * impactForce);
            }

            //we hit something, calling onHit method
            OnHit(_hit.point, _hit.normal);
        }
    }

    /*IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        weaponAnimator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime);

        weaponAnimator.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
    */
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
