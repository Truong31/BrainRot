using System;
using HellTap.PoolKit;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static Action disappear;

    public int level { get; private set; }
    public new string name { get; private set; }
    public I_Rarity rarity { get; private set; }
    public I_Style style { get; private set; }
    public Material material { get; private set; }
    public int cashPerSecond { get; private set; }
    private bool isGrabbed = false;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] Vector2 minMaxLifeTime;
    [SerializeField] TextMeshPro lifeTimeText;
    [SerializeField] TextMeshPro levelText;
    [SerializeField] TextMeshPro infoText;

    private float lifeTime;

    public void Init(int level, string name, I_Rarity rarity, I_Style style, Material material, bool isGrabbed)
    {
        this.level = level;
        this.name = name;
        this.rarity = rarity;
        this.style = style;
        this.material = material;
        this.isGrabbed = isGrabbed;
        meshRenderer.material = this.material;
        lifeTime = UnityEngine.Random.Range(minMaxLifeTime.x, minMaxLifeTime.y);
        if(isGrabbed) BeingGrabbed();
        else UpdateUI(this.level, this.name, this.rarity, lifeTime);
    }

    void UpdateUI(int? level = null, string name = null, I_Rarity? rarity = null, float? lifeTime = null)
    {
        levelText.text = "Level " + level;
        infoText.text = name + "\n" + rarity.ToString();
        lifeTimeText.text = lifeTime + "";
    }

    void Update()
    {
        if(isGrabbed) return;
        if(lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            lifeTimeText.text = (int)lifeTime + "";
        }
        else
        {
            disappear?.Invoke();
            GetComponent<Despawner>().Despawn();
        }
    }

    public void BeingGrabbed()
    {
        isGrabbed = true;
        disappear?.Invoke();
        UpdateUI();
    }
}
