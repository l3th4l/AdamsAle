using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

    [SerializeField]
    Transform target;
    [SerializeField]
    float smoothAmount = 5f;

    private Vector3 offset;

    float a;

    private void Start()
    {
        offset = transform.position - target.position;
        a = 0;
    }

    private void FixedUpdate()
    {
        a = (Input.GetAxisRaw("Horizontal")!=0)? Input.GetAxisRaw("Horizontal") : a;
        Vector3 M_off = new Vector3(offset.x * a, offset.y, offset.z);
        Vector3 targetCameraPos = target.position + M_off;
        transform.position = Vector3.Lerp(transform.position, targetCameraPos, smoothAmount * Time.deltaTime);
    }


}
