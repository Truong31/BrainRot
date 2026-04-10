using System;
using HellTap.PoolKit;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static Action disappear;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] Vector2 minMaxLifeTime;
    [SerializeField] TextMeshPro lifeTimeText;
    [SerializeField] TextMeshPro levelText;
    [SerializeField] TextMeshPro infoText;

    private float lifeTime;

    public void Init(int level, string name, I_Rarity rarity, Material material)
    {
        levelText.text = "Level " + level;
        infoText.text = name + "\n" + rarity.ToString();
        meshRenderer.material = material;
        lifeTime = UnityEngine.Random.Range(minMaxLifeTime.x, minMaxLifeTime.y);
        lifeTimeText.text = lifeTime + "";
    }

    void Update()
    {
        if(lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            lifeTimeText.text = (int)lifeTime + "";
        }
        else
        {
            //GetComponent<Despawner>().Despawn();
            disappear?.Invoke();
        }
    }

}
