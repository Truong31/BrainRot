using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum I_Rarity { Common, UnCommon, Rare }
public enum I_Style { Red, Blue, Yellow, Brown }

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable/Item")]
public class ItemData : ScriptableObject
{
#region Data
    [BoxGroup("Data")]
    public string itemName;
    [BoxGroup("Data")]
    public I_Rarity rarity;
    [BoxGroup("Data")]
    public I_Style style;
#endregion

#region  Visual
    [BoxGroup("Visual")]
    public Sprite sprite;
    [BoxGroup("Visual")]
    public List<Visual> visuals;

    [Serializable]
    public class Visual
    {
        public I_Style style;
        public Material material;

    }
#endregion

#region Rarity
    
    [BoxGroup("Rarity")]
    public List<IncomeStats> incomeStats;
    [Serializable]
    public class IncomeStats
    {
        public I_Rarity rarity;
        public int cashPerSecond;
        public int upgradeCost;
        public int upgradeCostMultiplier;
    }
#endregion

#region Logic

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

    public IncomeStats GetIncomeStats(I_Rarity rarity)
    {
        if(incomeStats == null) return null;
        foreach(IncomeStats i in incomeStats)
        {
            if(i.rarity == rarity)
                return i;
        }
        return null;
    }
#endregion
}
