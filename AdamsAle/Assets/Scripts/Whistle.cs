using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistle : MonoBehaviour
{
    public int step;
    public float res;
    public GameObject PixelObj;
    public float maxDistance;
    Vector3 targetPos;

    public int start;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse2))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            targetPos = (Camera.main.ScreenToWorldPoint(mousePos));
            targetPos.z = 0;
            Draw();
        }
        if(Input.GetKeyUp(KeyCode.Mouse2))
        {

           Collider[] EntInRad;
            EntInRad = Physics.OverlapSphere(transform.position + 0.75f * transform.up, Mathf.Clamp(Vector3.Magnitude(targetPos - transform.position - 0.75f * transform.up),0.0f, maxDistance));
            foreach(Collider Ent in EntInRad)
            {
                HostileAI AI = Ent.GetComponent<HostileAI>();
                if(AI != null)
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

    }

    void Draw()
    {
        /*for (int i = 0; i < Mathf.Clamp(Vector3.Magnitude(targetPos - transform.position - 0.75f * transform.up), 0, maxDistance) * res; i++)
        {
            if (i >= start)
                GameObject.Instantiate(PixelObj, transform.position + 0.75f * transform.up + i * (targetPos - transform.position - 0.75f * transform.up).normalized / res, Quaternion.identity);
        }*/

        for (int i = 0; i <= 360; i += step) 
        {
            float rad = Mathf.Clamp(Vector3.Magnitude(targetPos - transform.position - 0.75f * transform.up), 0, maxDistance);

            float Xpos = rad * Mathf.Cos(Mathf.Deg2Rad * i);
            float Ypos = rad * Mathf.Sin(Mathf.Deg2Rad * i);

            GameObject.Instantiate(PixelObj, transform.position + 0.75f * transform.up + new Vector3(Xpos,Ypos,0.0f) , Quaternion.identity);
        }

       // LN.SetWidth(0.1f, 0.0f);
       // LN.SetVertexCount(2);
       // LN.SetPosition(0, transform.position + 0.75f*transform.up);
       // LN.SetPosition(1, targetPos);
    }
}
