using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float radius = 3f;
    public Transform player;

    public float health = 50f;

    public GameObject L_Area;

    LightIntensity LClip;

    private void Start()
    {
        if (this.CompareTag("Light"))
        {
            LClip = L_Area.GetComponent<LightIntensity>();
            LClip.light = this.gameObject;
        }
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
        if (!this.CompareTag("Light"))
            Destroy(gameObject);
        else
        {
            if (LClip != null)
                LClip.Intensity = -3.00f;
            GetComponent<Light>().enabled = false;
        }
    }


}
