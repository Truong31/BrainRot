using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum B_Style { Normal, Golden, Diamond, Emerald, Ruby, Rainbow, Void, Ethereal, Celestial }
public enum B_Rare { Common, Rare, Epic,Legendary, Mythic,God, Secret }


[CreateAssetMenu(fileName = "BotData", menuName = "DATA/BotData", order = 0)]
public class BotData : ScriptableObject
{
    #region Identity

    [BoxGroup("Identity")]

    public int botId;
    public string botName = "New Bot";
    [BoxGroup("Identity")]
    public B_Rare Rarity = B_Rare.Common;

    [BoxGroup("Identity")]
    public B_Style DefaultStyle = B_Style.Normal;

    public float YAnchor;

    #endregion

    #region Visual

    [BoxGroup("Visual")]
    [PreviewField(60)]
    public Sprite icon;

    [BoxGroup("Visual")]
    public GameObject prefab;

    #endregion

    #region Style Visuals

    [Title("Style Visuals")]
    [TableList(AlwaysExpanded = true)]
    public StyleVisual[] styleVisuals = Array.Empty<StyleVisual>();

    [Serializable]
    public class StyleVisual
    {
        public B_Style style;

        [Header("Visual")]
        public Material material;
        public GameObject vfxPrefab;

        [Header("Spawn")]
        [PropertyRange(0, 1000)]
        public int spawnWeight = 1;
    }

    #endregion

    #region Logic

    public StyleVisual GetRandomStyleVisual(System.Random rng = null)
    {
        if (styleVisuals == null || styleVisuals.Length == 0) return null;

        var candidates = new List<StyleVisual>();
        var weights = new List<int>();

        foreach (var sv in styleVisuals)
        {
            if (sv == null || sv.material == null) continue;

            int w = Mathf.Max(0, sv.spawnWeight);
            if (w <= 0) continue;

            candidates.Add(sv);
            weights.Add(w);
        }

        if (candidates.Count == 0) return null;

        if (rng == null) rng = new System.Random();

        int total = 0;
        foreach (var w in weights) total += w;

        int rand = rng.Next(0, total);
        int acc = 0;

        for (int i = 0; i < candidates.Count; i++)
        {
            acc += weights[i];
            if (rand < acc)
                return candidates[i];
        }

        return candidates[candidates.Count - 1];
    }

    public Material GetMaterialForStyle(B_Style s)
    {
        foreach (var sv in styleVisuals)
        {
            if (sv != null && sv.style == s)
                return sv.material;
        }
        return null;
    }

    #endregion
}
public class BotStyleEntry
{
    public BotData botData;
    public BotData.StyleVisual styleVisual;

    public BotStyleEntry(BotData bot, BotData.StyleVisual style)
    {
        botData = bot;
        styleVisual = style;
    }
}
[Serializable]
public class StyleConfig
{
    public int style;

    public float baseIncome;
    public float incomePerLevel;
    public float upgradeCost;
    public float upgradeCostMultiplier;
}


[Serializable]
public class BotConfigEntry
{
    public int botId;
    public string botName;
    public List<StyleConfig> styles = new List<StyleConfig>();

    private Dictionary<int, StyleConfig> styleMap;

    public void BuildCache()
    {
        styleMap = new Dictionary<int, StyleConfig>();

        foreach (var s in styles)
        {
            if (!styleMap.ContainsKey(s.style))
                styleMap.Add(s.style, s);
        }
    }

    public StyleConfig GetStyle(B_Style style)
    {
        if (styleMap == null)
            BuildCache();

        styleMap.TryGetValue((int)style, out var s);
        return s;
    }
}