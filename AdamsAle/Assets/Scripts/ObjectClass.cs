using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClass : MonoBehaviour
{
    public CharacterClass ObjClass;

    public SpriteRenderer SR;

    private void Update()
    {
        if (SR != null)
            SR.sprite = ObjClass.CharSprite;
    }

    [HideInInspector]
    public bool isDetected = false;
}
