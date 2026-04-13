using System;
using System.Collections;
using System.Collections.Generic;
using HellTap.PoolKit;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SlotData
{
    public bool isOccupied;
    public GameObject currentItem;
    public Transform position;
}

public class IdleSlot : MonoBehaviour
{
    private bool isOccupied = false;
    private Transform currentItem;
    [SerializeField] private Transform itemPos;
    private PlayerManager player;
    private ItemInstance itemData;

    [Header("UI")]
    [SerializeField] Button placeItemBtn;
    [SerializeField] Button swapItemBtn;

    void Start()
    {
        Init();
    }

    void Init()
    {
        placeItemBtn.gameObject.SetActive(false);
        swapItemBtn.gameObject.SetActive(false);
    }

    private void PlaceItem(ItemInstance item)
    {
        if(isOccupied) return;
        Debug.Log("Place Item");
        isOccupied = true;
        itemData = item;
        currentItem = PoolKit.GetPool("MAIN").Spawn(item.itemName);
        currentItem.GetComponent<MeshRenderer>().material = item.GetMat();
        currentItem.SetParent(itemPos);
        currentItem.localPosition = Vector3.zero;
        currentItem.localRotation = Quaternion.identity;
        player.playerInventory.RemoveItem();
    }

    private void SwapItem(ItemInstance item)
    {
        if(!isOccupied) return;
        Debug.Log("Swap Item");
        isOccupied = false;
        PoolKit.GetPool("MAIN").Despawn(currentItem);
        ItemInstance i = itemData;
        PlaceItem(item);
        player.playerInventory.AddItem(i);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Find Slot");
            player = other.GetComponent<PlayerManager>();
            placeItemBtn.gameObject.SetActive(true);
            swapItemBtn.gameObject.SetActive(true);

            placeItemBtn.onClick.RemoveAllListeners();
            swapItemBtn.onClick.RemoveAllListeners();

            if(player.playerInventory.pickedObj != null)
            {
                placeItemBtn.onClick.AddListener(() =>
                {
                    PlaceItem(player.playerInventory.grabbedItem);
                });
                swapItemBtn.onClick.AddListener(() =>
                {
                    SwapItem(player.playerInventory.grabbedItem);
                });
                
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Cancel Slot");
            placeItemBtn.gameObject.SetActive(false);
            swapItemBtn.gameObject.SetActive(false);
            
            placeItemBtn.onClick.RemoveAllListeners();
            swapItemBtn.onClick.RemoveAllListeners();
            
            player = null;
        }
    }

    public void SaveSlotData()
    {
        
    }
    
}
