using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disguise : MonoBehaviour
{
    [SerializeField]
    CharacterClass PlayerClass;
    

    private void Update()
    {
        if (Input.GetKeyUp(UseKey))
            ProjectArms();
    }
    
    public Transform Arm;
    
    public KeyCode UseKey;

    [SerializeField]
    float armsLength;
    
    [SerializeField]
    LayerMask armsMask;

    void ProjectArms()
    {
        RaycastHit _PHit;
        if (Physics.Raycast(transform.position, transform.right, out _PHit, armsLength/*, armsMask*/))
        {

            CharacterClass DisguiseClass = _PHit.transform.GetComponent<ObjectClass>().ObjClass;

            Debug.Log(_PHit.transform.name);

            if (DisguiseClass != null)
            {
                _PHit.transform.GetComponent<ObjectClass>().ObjClass = GetComponent<ObjectClass>().ObjClass;
                GetComponent<ObjectClass>().ObjClass = DisguiseClass;
            }
        }
    }
}
