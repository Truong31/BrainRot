using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInstance
{
    public string itemName;
    public int currentLevel;
    public I_Style style;
    [NonSerialized] public ItemData itemData;

    public ItemInstance(ItemData itemData, int startLevel, I_Style style)
    {
        this.itemData = itemData;
        itemName = itemData.itemName;
        currentLevel = startLevel;
        this.style = style;
    }

    public void Upgrade() => currentLevel++;

    public float GetIncome()
    {
        var stats = itemData.GetIncomeStats(itemData.rarity);
        if (stats == null) return 0;

        return stats.cashPerSecond + (currentLevel * stats.upgradeCostMultiplier);
    }

    public Material GetMat()
    {
        return itemData.GetMaterial(style);
    }

}
