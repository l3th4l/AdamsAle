using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform PlayerSpawnPos;
    public GameObject PlayerPrefab;
    
	void Start ()
    {
        GameObject.Instantiate(PlayerPrefab, PlayerSpawnPos.position, PlayerPrefab.transform.rotation);		
	}
}
