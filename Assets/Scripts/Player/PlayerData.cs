using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<ItemData> items = new List<ItemData>();
    public ItemData grabbedItem;
    public VisualType visualType;
    public float speed;
    public float jumpForce;
    public int totalSlots;
}
