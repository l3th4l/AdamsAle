using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingSystem : MonoBehaviour {

    float t;
    Vector3 startPosition;
    Vector3 target;
    float timeToReachTarget;

    public Transform point1;
    public Transform point2;
    public float timeToReach= 1f;

    public GameObject nextHax;
    public Image progressImage;

    //public LineRenderer point1Line;
    //public Transform point1LinePos;
    //public LineRenderer point2Line;

    public GameObject hackCompletePanel;
    public GameObject hackFailedPanel;
    
    void Start()
    {
        startPosition = target = transform.position;
    }
    void Update()
    {
        //point1Line.SetPosition(0, transform.localPosition);
        //point1Line.SetPosition(1, point1LinePos.localPosition);

       

        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPosition, target, t);

        //SetDestination(point1.position, timeToReach);

        float dist1 = Vector3.Distance(transform.position, point1.position);
        float dist2 = Vector3.Distance(transform.position, point2.position);

        if (dist1 <= 2f)
        {
            SetDestination(point2.position, timeToReach);
        }
        else if(dist2 <=2f)
        {
            SetDestination(point1.position, timeToReach);
        }

        if(Input.GetMouseButtonDown(0) && dist1 <= 10f)
        {
            progressImage.color = Color.green;
            SetDestination(transform.position, 0f);

            if(nextHax != null)
            {
                nextHax.SetActive(true);
            }
            else
            {
                hackCompletePanel.SetActive(true);
            }
        }
        else if(Input.GetMouseButtonDown(0) && dist1 > 10f)
        {
            hackFailedPanel.SetActive(true);
        }

    }
    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        target = destination;
    }
}
