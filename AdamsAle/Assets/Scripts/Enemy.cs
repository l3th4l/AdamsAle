using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float radius = 3f;
    public Transform player;

    public float health = 50f;

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
        Destroy(gameObject);
    }


}
