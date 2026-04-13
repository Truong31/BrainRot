using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerUpgrade playerUpgrade;
    public PlayerVisual playerVisual;
    public PlayerAnim playerAnim;
    public PlayerInventory playerInventory;

    // void Start()
    // {
    //     LoadData();
    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Begin Grab");
            playerInventory.GrabObject();
        }
    }

#region Save/Load Data
    
    // void OnApplicationQuit()
    // {
    //     SaveData();
    // }

    // void SaveData()
    // {
    //     PlayerData playerData = new PlayerData();
    //     playerData.speed = playerMovement.speed;
    //     playerData.jumpForce = playerMovement.jumpForce;
    //     playerData.visualType = playerVisual.visualType;
    //     playerData.totalSlots = playerInventory.totalSlots;
    //     playerData.items = playerInventory.totalItems;

    //     if(playerInventory.grabbedItem != null)
    //         playerData.grabbedItem = playerInventory.grabbedItem;
    //     else
    //         playerData.grabbedItem = null;

    //     string str = JsonUtility.ToJson(playerData);
    //     PlayerPrefs.SetString("Player", str);
    // }
    
    // void LoadData()
    // {
    //     string str = PlayerPrefs.GetString("Player", null);
    //     PlayerData playerData = JsonUtility.FromJson<PlayerData>(str);
    //     if(playerData != null)
    //     {
    //         playerMovement.Init(playerData.speed, playerData.jumpForce);
    //         playerVisual.Init(playerData.visualType);
    //         playerInventory.Init(playerData.totalSlots, playerData.items, playerData.grabbedItem);
    //     }
    // }

#endregion
}
