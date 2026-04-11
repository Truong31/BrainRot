using System.Collections;
using System.Collections.Generic;
using HellTap.PoolKit;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int totalSlots { get; private set; } = 3;
    public List<ItemData> totalItems { get; private set; } = new List<ItemData>();
    public ItemData grabbedItem { get; private set; }
    private int currentSlots = 0;
    private GameObject waitObj;
    [SerializeField] private Transform pickedObj;

    public void Init(int totalSlots, List<ItemData> list, ItemData itemData)
    {
        this.totalSlots = totalSlots;
        totalItems = list;
        grabbedItem = itemData;
        
        if(pickedObj == null)
        {
            Debug.LogError("PlayerInventory: pickedObj is not assigned in the Inspector!");
            return;
        }
        
        if(pickedObj.childCount == 0 && grabbedItem != null)
        {
            Pool pool = PoolKit.GetPool("MAIN");
            if(pool == null)
            {
                Debug.LogError("PlayerInventory: Failed to get MAIN pool from PoolKit!");
                return;
            }
            
            Debug.Log(grabbedItem.itemName);
            Transform obj = pool.Spawn(grabbedItem.itemName);
            if(obj == null)
            {
                Debug.LogError($"PlayerInventory: Failed to spawn item {grabbedItem.itemName}!");
                return;
            }
            
            obj.parent = pickedObj;
            obj.localPosition = Vector3.zero;
            obj.rotation = Quaternion.identity;
            Item itemComponent = obj.GetComponent<Item>();
            if(itemComponent != null)
            {
                itemComponent.Init(grabbedItem.level, grabbedItem.itemName, grabbedItem.rarity, grabbedItem.style, grabbedItem.material, true);
            }
            else
            {
                Debug.LogError($"PlayerInventory: Spawned item {grabbedItem.itemName} doesn't have Item component!");
            }
        }
    }

    public void GrabObject()
    {
        if(currentSlots >= totalSlots)
        {
            Debug.Log("Out of Slot");
            return;
        }
        if(waitObj != null && currentSlots < totalSlots)
        {
            Debug.Log("Grab obj success");
            currentSlots++;

            ItemData item = new ItemData();
            Item i = waitObj.GetComponent<Item>();
            if(i == null)
            {
                Debug.LogError("GrabObject: waitObj doesn't have Item component!");
                return;
            }

            item.level = i.level;
            item.itemName = i.name;
            item.rarity = i.rarity;
            item.style = i.style;
            item.material = i.material;
            totalItems.Add(item);
            i.BeingGrabbed();

            if(pickedObj == null)
            {
                Debug.LogError("GrabObject: pickedObj is null!");
                return;
            }

            if(pickedObj.childCount == 0)
            {
                grabbedItem = item;
                waitObj.transform.parent = pickedObj;
                waitObj.transform.localPosition = Vector3.zero;
                waitObj.transform.rotation = Quaternion.identity;
                waitObj = null;
            }
            else
            {
                Despawner despawner = waitObj.GetComponent<Despawner>();
                if(despawner != null)
                {
                    despawner.Despawn();
                }
                else
                {
                    Debug.LogError("GrabObject: waitObj doesn't have Despawner component!");
                }
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("Find Item");
            if(waitObj == null)
                waitObj = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("Cancel Item");
            if(waitObj != null) waitObj = null;
        }
    }

}
