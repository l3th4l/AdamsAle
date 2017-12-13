using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour 
{
	GameObject player;
	public Camera AICam;
	Vector3 PlayerPos;

	public int WalkSpeed;
	public int RunSpeed;
	public float TooNearDist;
	public float RunSusp;
	public float WalkSusp;

	Plane[] CamPlanes;
	Collider PlayerCollider;

	public float PatrolDistance;
	public float AwarePatrolDistance;

	public Animator AIAnimControl;
	Vector3 InitialPos;
	Rigidbody RB;
	Rigidbody RBx;
	Rigidbody PlayerRB;

	float Isleft;
	bool flippable;
	bool Aware;

	Vector3 KnownPos;
	Enemy HealthScr;

	float InitalHealth;
	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 20f;
	public float SuspFarClipIncrease;
	public float DetectedFarClipIncrease;

    public bool Alarmed = false;
    public bool inZone = false;

    private float EndCh;
    private float grDist;


	[SerializeField]
	[Header("Weapon Info")]
	private Transform weaponTransform;
	public GameObject hitEffectPrefab;
	public ParticleSystem muzzleFlash;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		PlayerCollider = player.GetComponent<CapsuleCollider> ();
		AIAnimControl = this.gameObject.GetComponent<Animator> ();
		RB = GetComponent<Rigidbody> ();
		RBx = GetComponent<Rigidbody> ();
		PlayerRB = player.GetComponent<Rigidbody> ();
		InitialPos = transform.position;
		flippable = true;
		KnownPos = Vector3.zero;
		Aware = false;
		HealthScr = GetComponent<Enemy>();
		InitalHealth = HealthScr.health;
        EndCh = GetComponent<CapsuleCollider>().radius * 1.5f;
        RaycastHit g_hit;
        if (Physics.Raycast(transform.position, Vector3.up * -1, out g_hit))
            grDist = Vector3.SqrMagnitude(g_hit.point - transform.position);
	}

	void Update () 
	{
        Debug.DrawRay(InitialPos, transform.up, Color.cyan);
        


        Vector3 castPos = transform.position + new Vector3(EndCh, 0.0f, 0.0f) * Isleft;

        RaycastHit f_check;

        if (Physics.Raycast(castPos, transform.up * -1, out f_check))
        {
            if (Vector3.SqrMagnitude(f_check.point - castPos) > grDist)
            {
                if (AIAnimControl.GetCurrentAnimatorStateInfo(0).IsName("Patrol R") || AIAnimControl.GetCurrentAnimatorStateInfo(0).IsName("AwarePatrolR"))
                {
                    Debug.Log("Stap muttafukkaaaaaa!!!!");
                    AIAnimControl.SetInteger("MaxDist", 1);
                    transform.position = transform.position - new Vector3(0.4f * Isleft, 0.0f, 0.0f);
                    InitialPos = castPos - new Vector3(PatrolDistance * 1.01f * Isleft, 0.0f, 0.0f);
                }
            }
        }

        if (inZone && player.activeInHierarchy)
            PlayerPos = player.transform.position;

		CamPlanes = GeometryUtility.CalculateFrustumPlanes (AICam);

		if (GeometryUtility.TestPlanesAABB (CamPlanes, PlayerCollider.bounds) && player.activeInHierarchy) {
			OnDetect ();
			//print ("Detected Muttafuka!");
		} else {
			AIAnimControl.SetBool ("Detected", false);
			//print ("were det muttafuka?");
		}
		Suspicious ();

		Isleft = transform.localScale.x ;

		if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("Go R")) 
		{
            //print (KnownPos.x + "" + transform.position.x);
			if ((KnownPos.x - transform.position.x) * (KnownPos.x - transform.position.x) < 0.25) 
			{
				AIAnimControl.SetTrigger ("NfoundAtLP");
				InitialPos = KnownPos;
				//print ("NotAtLP");
			}

			Aware = true;
		}			
		if(AIAnimControl.GetCurrentAnimatorStateInfo(0).IsTag("SoundD")){
            if (inZone)
            {
                if (Vector3.SqrMagnitude(PlayerPos - transform.position) < RunSusp * RunSusp)
                {
                    if (Vector3.SqrMagnitude(PlayerPos - transform.position) < WalkSusp * WalkSusp)
                    {
                        if (PlayerRB.velocity.x >= 4.0f)
                            AIAnimControl.SetBool("Detected", true);
                    }
                    else
                    {
                        if (PlayerRB.velocity.x >= 8.0f)
                        {
                            if ((((transform.position - PlayerPos).x < 0) ? -1 : 1) - Isleft == 0)
                                transform.localScale = new Vector3(-1 * Isleft, 1.0f, 1.0f);
                            AIAnimControl.SetBool("Detected", true);
                        }
                    }
                }
            }
		}

		if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			if ( AIAnimControl.GetCurrentAnimatorStateInfo (0).normalizedTime >0.95)
				Shoot ();
			//print ("KYS!!!!!");
			AIAnimControl.SetBool ("Aware", false);
		}

		if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("Go R")) {
			Run ();
			AIAnimControl.SetBool ("Aware", false);
		}

		if (HealthScr.health != InitalHealth) {
			AIAnimControl.SetBool ("Aware", true);
			KnownPos = PlayerPos;
			InitalHealth = HealthScr.health;
		}

		if (player == null)
			AIAnimControl.SetBool ("Detected", false);

        if(Alarmed)
        {
            AIAnimControl.SetBool("Aware", true);

            KnownPos = PlayerPos;
        }

	}

	void Patrol()
	{

        if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("Patrol R")) {
			if ((transform.position.x - InitialPos.x) * (transform.position.x - InitialPos.x) >= PatrolDistance * PatrolDistance) {
				AIAnimControl.SetInteger ("MaxDist", 1);
				transform.position = transform.position - new Vector3 (0.4f * Isleft, 0.0f, 0.0f);
			}
		}
		if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("Stop Patrol")) 
		{			
			if (AIAnimControl.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.9)
			{
				if (flippable) {
					transform.localScale = new Vector3 (-1 * Isleft, 1.0f, 1.0f);
					flippable = false;
					AIAnimControl.SetInteger ("MaxDist", -1);
				}
			}
		}

		if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("Patrol R")) 
		{
			if (AIAnimControl.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.5)
				flippable = true;
			Walk ();
		}
	}
	void AwarePatrol()
	{

        
        if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("AwarePatrolR")) {
			if ((transform.position.x - InitialPos.x) * (transform.position.x - InitialPos.x) >= AwarePatrolDistance * AwarePatrolDistance) {
				AIAnimControl.SetInteger ("MaxDist", 1);
				transform.position = transform.position - new Vector3 (0.4f * Isleft, 0.0f, 0.0f);
			}
		}
		if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("Stop Patrol 1")) 
		{

			if (AIAnimControl.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.9)
			{
				if (flippable) {
					transform.localScale = new Vector3 (-1 * Isleft, 1.0f, 1.0f);
					flippable = false;
					AIAnimControl.SetInteger ("MaxDist", -1);
				}
			}
		}

		if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("AwarePatrolR")) 
		{
			if (AIAnimControl.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.5)
				flippable = true;
			Walk ();
		}
	}
	void Walk()
	{
		float a = RB.velocity.y;
		RB.velocity = new Vector3 (WalkSpeed * 1.0f * Isleft, a, 0.0f);
	}
	void Run()
	{
		float a = RB.velocity.y;
		RB.velocity = new Vector3 (RunSpeed * 1.0f * Isleft, a, 0.0f);
				if ((((transform.position - PlayerPos).x < 0) ? -1 : 1) - Isleft == 0)
					transform.localScale = new Vector3 (-1 * Isleft, 1.0f, 1.0f);
	}

	void OnDetect()
	{
		AIAnimControl.SetBool ("Detected", true);
        if (inZone)
            KnownPos = PlayerPos;

	}
	void Suspicious()
	{
		if(AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("SSP")||AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("Look")||AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("Attack"))
		{
			if (AIAnimControl.GetBool ("Detected")) 
			{
				if ((((transform.position - PlayerPos).x < 0) ? -1 : 1) - Isleft == 0)
					transform.localScale = new Vector3 (-1 * Isleft, 1.0f, 1.0f);
			}
		}
		if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("SSP")) {
			if (Vector3.SqrMagnitude (PlayerPos - transform.position) < TooNearDist * TooNearDist)
				AIAnimControl.SetBool ("TooNear", true);
			if (AIAnimControl.GetCurrentAnimatorStateInfo (0).IsName ("SSP") && AIAnimControl.GetCurrentAnimatorStateInfo (0).normalizedTime > 1)
				AIAnimControl.SetBool ("TooNear", true);
		} else 
		{
			AIAnimControl.SetBool ("TooNear", false);
		}
	}
	
	void FixedUpdate()
	{
		if (!Aware)
			Patrol ();
		else
			AwarePatrol ();

	}
	void Shoot()
	{
		OnShoot();

		RaycastHit _hit;
		if (Physics.Raycast(weaponTransform.position, weaponTransform.forward, out _hit, range))
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
	void OnHit(Vector3 _hitPos, Vector3 _normal)
	{

		//Hit Effect
		GameObject hitEffect = (GameObject)Instantiate(hitEffectPrefab, _hitPos, Quaternion.LookRotation(_normal));
		Destroy(hitEffect, 2f);
	}

	void OnShoot()
	{
		//Shoot Effect
		muzzleFlash.Play();
	}
}
