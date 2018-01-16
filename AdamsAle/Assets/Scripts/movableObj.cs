using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movableObj : MonoBehaviour
{
    [SerializeField]
    float weight = 1;

    public float speed;
    private void Start()
    {
        speed = 1 / weight;
    }
    public void push(Vector3 force)
    {
        transform.Translate(force);
    }
}
