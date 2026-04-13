using System;
using System.Collections.Generic;
using HellTap.PoolKit;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static Action SpawnNewItem;
    
    [SerializeField] Button getItemBtn;
    public int totalSlots { get; private set; } = 3;
    
    public List<ItemInstance> totalItems { get; private set; } = new List<ItemInstance>();
    public ItemInstance grabbedItem { get; private set; }
    
    private int currentSlots = 0;
    
    private Item waitItem; 
    
    public Transform grabbedItemPos;
    public Transform pickedObj { get; private set; }

    public void Init(int totalSlots, List<ItemInstance> list, ItemInstance grabbedItem)
    {
        this.totalSlots = totalSlots;
        this.totalItems = list ?? new List<ItemInstance>();
        this.grabbedItem = grabbedItem;
        GetItem();
    }

    void Start()
    {
        getItemBtn.onClick.AddListener(GetItem);
    }

    void OnDisable()
    {
        getItemBtn.onClick.RemoveListener(GetItem);
    }

    void GetItem()
    {
        if(grabbedItem == null || grabbedItem.itemData == null)
        {
            if(totalItems.Count == 0) return;
            grabbedItem = totalItems[0];
        }

        if(grabbedItem != null && grabbedItem.itemData != null && grabbedItemPos != null)
        {
            GetItemModel(grabbedItem, grabbedItemPos);
        }
    }

    public void GrabObject()
    {
        if(currentSlots >= totalSlots)
        {
            Debug.Log("Out of Slot");
            return;
        }

        if(waitItem != null && currentSlots < totalSlots)
        {
            Debug.Log("Grab obj success");
            
            ItemInstance grabbedInstance = waitItem.myInstance; 
            
            AddItem(grabbedInstance);
            PoolKit.GetPool("MAIN").Despawn(waitItem.transform);
            waitItem = null; 
            
            SpawnNewItem?.Invoke();
        }
    }

    public void RemoveItem()
    {
        if (grabbedItem == null) return;

        currentSlots--;
        
        if (totalItems.Contains(grabbedItem))
        {
            totalItems.Remove(grabbedItem);
        }

        if (pickedObj != null)
        {
            PoolKit.GetPool("MAIN").Despawn(pickedObj);
            pickedObj = null;
        }
        
        grabbedItem = null;
    }

    public void AddItem(ItemInstance itemInstance) 
    {
        currentSlots++;
        totalItems.Add(itemInstance);
        
        if(grabbedItem == null)
        {
            grabbedItem = itemInstance;
            GetItemModel(grabbedItem, grabbedItemPos);
        }
    }

    void GetItemModel(ItemInstance instance, Transform parent)
    {
        if(instance == null || instance.itemData == null || string.IsNullOrEmpty(instance.itemData.itemName)) return;

        pickedObj = PoolKit.GetPool("MAIN").Spawn(instance.itemData.itemName);

        MeshRenderer renderer = pickedObj.GetComponent<MeshRenderer>();
        
        if(renderer != null)
        {
            renderer.material = instance.GetMat(); 
        }
        
        pickedObj.SetParent(parent);
        pickedObj.localPosition = Vector3.zero;
        pickedObj.localRotation = Quaternion.identity;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item itemComponent = other.GetComponent<Item>();
            if (itemComponent != null)
            {
                Debug.Log("Find Item");
                if(waitItem == null)
                    waitItem = itemComponent;
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item itemComponent = other.GetComponent<Item>();
            if (itemComponent != null && waitItem == itemComponent)
            {
                Debug.Log("Cancel Item");
                waitItem = null;
            }
        }
    }
}