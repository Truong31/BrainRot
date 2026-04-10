using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    [Header("Pick Object")]
    private GameObject waitObj;
    private GameObject pickedObj;

    void GrabObject()
    {
        if(waitObj != null && pickedObj == null)
        {
            waitObj.transform.position = pickedObj.transform.position;
            pickedObj = waitObj;
            waitObj = null;
        }
    }
    
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Target"))
    //     {
    //         Character character = other.GetComponentInParent<Character>();
    //         if(waitObj == null)
    //             waitObj = character.gameObject;
    //     }
    // }
    // void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Target"))
    //     {
    //         if(waitObj != null) waitObj = null;
    //     }
    // }

}
