using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static Action a;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerUpgrade playerUpgrade;
    [SerializeField] PlayerVisual playerVisual;
    [SerializeField] PlayerAnim playerAnim;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(""))
        {
            a?.Invoke();
        }
    }

}
