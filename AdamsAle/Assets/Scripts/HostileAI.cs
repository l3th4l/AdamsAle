using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileAI : MonoBehaviour
{
    [Header("Private Variables")]
    Camera AICam;
    Plane[] CamPlanes;
    Collider Player;
    Rigidbody RB;


    private float LOS_Time = 0.0f;// Time passed since entered LOS
    private float Search_Time = 0.0f;// TIme passed since searching player's last position
    private float Distracted_Time = 0.0f;// Time passed since Distracted  
    private float Explore_Time = 0.0f;// Time passed since Exploring

    [Space]
    [Header("Parameteres")]
    public float PassedTime = 0.0f; //Time passes since last patrol clip
    public float WalkVelocity;
    public float RunVelocity;
    public float maxDisp;//Maximum patrol distance
    public float maxWaitTime;// Time for which Entity waits before turning back and patrolling again
    public float stopDistance;// Stopping distance from a point
    public float suspectTime;// Time spent in line of sight before detection
    public float maxDistractedTime;// Time before Entity reacts to distraction
    public float maxExploreTime;// Time for which Entity stays at distraction object's position
    public float maxSearchTime;// TIme for which Entity swats at player's last known position

    public LayerMask RaycastMask;

    private float maxWalkTime;// Time for which the Entitiy patrols before stopping and turning back 

    public float maxCamAngle; // maximum rotation of camera for looking around
    public float maxSearchAngle; // maximum rotation of camera while searching

    public bool detected = false;
    public bool distracted = false;
    public bool aware = false;

    public bool inSearch = false;

    public Vector3 distractionPos;
    public Vector3 knownPos;
    [Space]
    [Header("Aware vars")]
    public float A_SuspectTime;
    public float A_MaxDistractedTime;
    public float A_MaxExploreTime;

    [Space]
    [Header("Test variables")]
    public Transform TestTransform;

    // Use this for initialization
    void Start()
    {
        RB = this.GetComponent<Rigidbody>();
        maxWalkTime = maxDisp / WalkVelocity;
        AICam = transform.GetChild(0).GetComponentInChildren<Camera>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }
    
    // Update is called once per frame
    void Update()
    {

        if(aware) // Set everything to aware state
        {
            suspectTime = A_SuspectTime;
            maxDistractedTime = A_MaxDistractedTime;
            maxExploreTime = A_MaxExploreTime;
        }

        bool PlayerNotObstructed = true;// Variable to check if player is obstructed or not 
        RaycastHit _P_Hit;// Player hit info
        if (Physics.Raycast(AICam.transform.position, (Player.transform.position - AICam.transform.position).normalized, out _P_Hit, RaycastMask))// Raycasts to the player
        {
            PlayerNotObstructed = (_P_Hit.collider.CompareTag("Player")|| (Vector3.SqrMagnitude((transform.position - Player.transform.position).x*Vector3.right) <= AICam.nearClipPlane*AICam.nearClipPlane && Player.gameObject.activeInHierarchy) ) ;// If player isn't obstructed, becomes true
        }
            


        PassedTime += Time.deltaTime;// increases with time
        CamPlanes = GeometryUtility.CalculateFrustumPlanes(AICam);
        if (GeometryUtility.TestPlanesAABB(CamPlanes, Player.bounds) && Player.gameObject.activeInHierarchy && PlayerNotObstructed)// Checks if player is in LOS
        {
            LOS_Time += Time.deltaTime;// Time after being in LOS
            inSearch = false;

            if (detected)// Shoots if detected and in LOS
            {
                /////////////////////////// Shooting look
                float Angle = Vector3.Angle(transform.right, (Player.transform.position - transform.position).normalized);// Angle between Camera and player's position
                AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Clamp(Mathf.Round(Angle/4)*4, -maxSearchAngle, 90)); // makes the camera look at player's position
                ///////////////////////////
                if (Angle >= 90)
                    transform.Rotate(Vector3.up * 180);

                knownPos = Player.transform.position;// Updates last known position if player is in line of sight
                PassedTime = 0.0f;//Reset

                //Shoot
                if (Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / fireRate;
                    Shoot(knownPos);// Shoots at the player
                }

                AICam.GetComponent<ViewAdjust>().ViewType = 2;// Camera View type
            }
            else
            {
                PassedTime -= Time.deltaTime;

                /////////////////////////// Suspicious look
                float Angle = Vector3.Angle(transform.right, (Player.transform.position - transform.position).normalized);// Angle between Camera and player's position
                AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Clamp(Mathf.Round(Angle/4)*4, -maxSearchAngle, 90)); // makes the camera look at player's position
                ///////////////////////////
                if (Angle >= 90)
                    transform.Rotate(Vector3.up * 180);

                if (LOS_Time >= suspectTime)// detects the player if player in LOS for >= suspectTime
                {
                    detected = true;
                    aware = true; // Alerts the entity. [Changes all parameteres to aware state]
                    knownPos = Player.transform.position;// Sets the known position of player 
                }

                AICam.GetComponent<ViewAdjust>().ViewType = 4;// Camera View type
            }
            distracted = false;
        }
        else
        {
            LOS_Time = 0.0f;
            if (detected)// If player is detected
            {
                inSearch = true;
                distracted = false;//Reset
                Distracted_Time = 0.0f;//Reset
                Explore_Time = 0.0f;//Reset

                if (Vector3.SqrMagnitude((knownPos - transform.position).x * Vector3.right) >= stopDistance * stopDistance)
                {
                    Search_Time = 0.0f;//Reset
                    RunTo(knownPos,RunVelocity);
                }
                else
                {
                    Search_Time += Time.deltaTime;

                    Search(false, knownPos, maxSearchTime, Search_Time);

                    if (Search_Time >= maxSearchTime * 0.75)// If Entitity has finished searching and couldn't find player, reset to undetected                        
                        AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                    if (Search_Time >= maxSearchTime)// If Entitity has finished searching and couldn't find player, reset to undetected 
                    {
                        AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                        transform.Rotate(transform.up, 180);// Flips the entity

                        detected = false;
                    }

                }

                AICam.GetComponent<ViewAdjust>().ViewType = 2;// Camera View type

            }
            else
            {
                inSearch = false;
                Search_Time = 0.0f;// Reset

                if (distracted)// If Entity is distracted by an object
                {
                    Debug.DrawRay(distractionPos, Vector3.up);
                    Distracted_Time += Time.deltaTime;// Time after being distracted
                    if (Distracted_Time >= maxDistractedTime)
                    {
                        if (Vector3.SqrMagnitude((distractionPos - transform.position).x*Vector3.right) >= stopDistance * stopDistance)// if the Entitity is not near to the distraction object
                        {
                            Explore_Time = 0.0f;//Reset
                            if (!aware)
                                RunTo(distractionPos,WalkVelocity);// Walks up to the distraction object if Entity is unaware
                            else
                                RunTo(distractionPos,RunVelocity);// Runs up to the distraction object if Entitiy is aware

                            /////////////////////////// Distracted look
                            float Angle = Vector3.Angle(transform.right, (distractionPos + transform.right * AICam.farClipPlane - transform.position).normalized);// Angle between Camera and distraction position
                            if (Angle > 90)
                                transform.Rotate(transform.up, 180);
                            AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Clamp(Mathf.Round(Angle / 4) * 4, -maxSearchAngle, 90)); // makes the camera look at distraction position
                            Debug.Log(Angle);
                            ///////////////////////////

                        }
                        else
                        {
                            RB.velocity = RB.velocity.y*Vector3.up + Vector3.zero;//Stops movement when Entity reaches distraction Pos
                            Explore_Time += Time.deltaTime;// Time after starting exploring

                            Search(true, distractionPos, maxExploreTime, Explore_Time);

                            if (Explore_Time > maxExploreTime)// End exploring after maxExploreTime
                                distracted = false;// Set entity to not distract after completing exploration 
                        }
                    }
                    
                    AICam.GetComponent<ViewAdjust>().ViewType = 3;// Camera View type
                }
                else
                {
                    Distracted_Time = 0.0f;
                    detected = false;
                    Patrol();//Patrol if player is not detected not is the Entity distracted

                    AICam.GetComponent<ViewAdjust>().ViewType = 1;// Camera View type
                }
            }
        }

    }
    void Patrol()// Patrolling Entity state
    {
        if (PassedTime <= maxWalkTime)
        {
            // Walk while Passed time <= Walking time
            RB.velocity = RB.velocity.y*Vector3.up + transform.right * WalkVelocity;

            AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Sin(PassedTime * 5) * maxCamAngle / 5);//Camera wiggle
        }
        else
        {
            AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Sin(PassedTime *2) * maxCamAngle / 8);//Camera wiggle

            if (PassedTime <= (maxWalkTime) + maxWaitTime)
            {
                // Stop for the waiting time
                RB.velocity = RB.velocity.y*Vector3.up + Vector3.zero;
            }
            else                                          //Turn and walk again
            {
                PassedTime = 0.0f; //Reset passed time to restart the walkign state
                transform.Rotate(Vector3.up * 180); // Rotates by 180 degrees on y axis
            }
        }
    }

    void RunTo(Vector3 Point, float Velocity)
    {
        if ((transform.right.x == (Point - transform.position).normalized.x))
            transform.Rotate(Vector3.up * 180);
        if (Vector3.SqrMagnitude((Point - transform.position).x * Vector3.right) >= stopDistance * stopDistance) // Chase if entity isn't "StopDistance" away from target
        {
            RB.velocity = RB.velocity.y * Vector3.up + ((((Point - transform.position).x > 0) ? 1 : -1) * Vector3.right) * Velocity;
        }
        else
        {
            RB.velocity = RB.velocity.y * Vector3.up + Vector3.zero; // Stop if within "StopDistance" 
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //For shooting

    [Space]
    [Header("For Shooting")]
    float nextTimeToFire = 0.0f;
    public float fireRate;
    public float range = 100f;
    public float damage = 10f;

    [SerializeField]
    public LayerMask mask;


    [Space]
    [Header("Effects")]
    public float impactForce = 20f;
    public GameObject hitEffectPrefab;
    public GameObject muzzleFlashPrefab;

    void Shoot(Vector3 target)
    {
        //Debug.Log("Shoot" + nextTimeToFire);

        //Debug.Log("Shooting");

        //We are shooting, calling OnShoot method
        OnShoot();

        RaycastHit _hit;
        Debug.Log("Hit created" + target);

        Debug.DrawRay(AICam.transform.position, (target - AICam.transform.position).normalized, Color.red);

        if (Physics.Raycast(AICam.transform.position, (target - AICam.transform.position).normalized, out _hit, range))
        {

            Debug.Log("Hit" + target);

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

        //Hit Effect
        GameObject MuzzleFlash = (GameObject)Instantiate(muzzleFlashPrefab, AICam.transform.position+ transform.right*AICam.nearClipPlane, Quaternion.LookRotation(AICam.transform.right));
        Destroy(MuzzleFlash, 2f);
    }

    bool flip = true;
    unsafe void Search(bool Partial, Vector3 SearchPos, float Duration, float Time)
    {
        if (Time == UnityEngine.Time.deltaTime)
            flip = true;

        if (Partial)
        {
            if (Time <= Duration)
            {

                /////////////////////////// Partial search
                float Angle = Vector3.Angle(transform.right, (SearchPos + transform.right * AICam.farClipPlane - transform.position).normalized);// Angle between Camera and pos
                if (Angle > 90)
                    transform.Rotate(transform.up, 180);
                AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Clamp(Mathf.Round((Angle + Mathf.Clamp((Mathf.Sin((Time / Duration * 8.38f) - 2.618f) + 0.5f) * 4, 0, 1) * 30) / 10) * 10, -maxSearchAngle, 90)); // makes the camera look at pos
                ///////////////////////////
            }
        }
        else
        {

            /////////////////////////// Complete search
            float Angle = Vector3.Angle(transform.right, (SearchPos + transform.right * AICam.farClipPlane - transform.position).normalized);// Angle between Camera and pos
            if (Time >= Duration / 2 && flip)
                transform.Rotate(transform.up, 180);
            AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Clamp(Mathf.Round((Angle + Mathf.Clamp((Mathf.Sin((Time / Duration * 8.38f * 2f) - 2.618f) + 0.5f) * 4, 0, 1) * 30) / 10) * 10, -maxSearchAngle, 90)); // makes the camera look at pos
            ///////////////////////////

            if (Time >= Duration / 2)
                flip = false;
        }
    }
        
}
