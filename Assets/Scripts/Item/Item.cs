using System;
using HellTap.PoolKit;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static Action disappear;
    
    public ItemInstance myInstance;
    private Transform model;

    [SerializeField] private Transform modelParent;
    [SerializeField] Vector2 minMaxLifeTime;
    [SerializeField] TextMeshPro lifeTimeText;
    [SerializeField] TextMeshPro levelText;
    [SerializeField] TextMeshPro infoText;
    [SerializeField] TextMeshPro cashPerSecondText;

    private float lifeTime;

    public void Init(ItemInstance instance , Material material, Transform model)
    {
        myInstance = instance;
        this.model = model;
        this.model.SetParent(modelParent);
        this.model.localPosition = Vector3.zero;
        this.model.localRotation = Quaternion.identity;
        this.model.GetComponent<MeshRenderer>().material = material;

        lifeTime = UnityEngine.Random.Range(minMaxLifeTime.x, minMaxLifeTime.y);
        UpdateUI(myInstance.currentLevel, myInstance.itemName, myInstance.itemData.rarity, myInstance.GetIncome(), lifeTime);
    }

    void UpdateUI(int level, string name, I_Rarity rarity, float cashPerSecond, float lifeTime)
    {
        levelText.text = "Level " + level;
        cashPerSecondText.text = cashPerSecond + "$";
        infoText.text = name + "\n" + rarity.ToString();
        lifeTimeText.text = lifeTime + "";
        
    }

    void Update()
    {
        if(lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            lifeTimeText.text = (int)lifeTime + "";
        }
        else ReturnPool();
    }

    void ReturnPool()
    {
        disappear?.Invoke();
        PoolKit.GetPool("MAIN").Despawn(model);
        PoolKit.GetPool("MAIN").Despawn(transform);
    }

}
