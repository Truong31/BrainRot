using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum I_Rarity { Common, UnCommon, Rare }
public enum I_Style { Red, Blue, Yellow, Brown }

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable/Item")]
public class ItemScriptable : ScriptableObject
{
#region Data
    [BoxGroup("Data")]
    public int ID;
    [BoxGroup("Data")]
    public string itemName;
    [BoxGroup("Data")]
    public I_Rarity rarity;
    [BoxGroup("Data")]
    public I_Style style;
    [BoxGroup("Data")]
    public GameObject prefab;
#endregion

#region  Visual
    [BoxGroup("Visual")]
    public List<Visual> visuals;
    [Serializable]
    public class Visual
    {
        public I_Style style;
        public Material material;

    }
#endregion

#region Logic

    public I_Rarity GetRarity()
    {
        return (I_Rarity)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(I_Rarity)).Length);
    }

    public Visual GetRandomVisual()
    {
        if(visuals == null) return null;
        return visuals[UnityEngine.Random.Range(0, visuals.Count)];
    }

    public Material GetMaterial(I_Style style)
    {
        if(visuals == null) return null;
        foreach(Visual v in visuals)
        {
            if(style == v.style && v != null)
                return v.material;
        }
        return null;
    }
#endregion
}

public class ItemStyleEntry
{
    public ItemScriptable itemData;
    public ItemScriptable.Visual styleVisual;

    public ItemStyleEntry(ItemScriptable item, ItemScriptable.Visual style)
    {
        itemData = item;
        styleVisual = style;
    }
}

[Serializable]
public class IncomeStats
{
    public I_Style rarity;
    public int cashPerSecond;
    public int baseIncome;
    public int upgradeCost;
}
