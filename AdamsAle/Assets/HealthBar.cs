using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Enemy PlayerHealth;

    [Space]
    [Header("HU Sprites")]
    public Sprite Active;
    public Sprite Half;
    public Sprite Inactive;

    int HU_count;
    GameObject[] HUs;
    
	void Start ()
    {
        HU_count = transform.GetChildCount();
        HUs = new GameObject[HU_count];
        
        for(int i = 0; i< HU_count; i++)
        {
            HUs[i] = transform.GetChild(i).gameObject;
            if (PlayerHealth.health <= i * 50)
                HUs[i].SetActive(false);
        }
	}
	
	void Update ()
    {
        for (int i = 0; i < HU_count; i++)
        {
            HUs[i].GetComponent<Image>().sprite = (PlayerHealth.health - i * 50 > 0) ? (PlayerHealth.health - i * 50 > 25) ? Active : Half : Inactive;
        }
    }
}
