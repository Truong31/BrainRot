using System.Collections.Generic;
using System.Linq;
using HellTap.PoolKit;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] int totalSpawn;
    private Vector2 maxSpawnPos;
    private Vector2 minSpawnPos;
    [SerializeField] float rayCastLength = 6f;
    [SerializeField] int tryCount = 10;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float distance = 5f;
    private List<Vector3> spawnPos = new List<Vector3>();
    private new Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
        SpawnArea();
        InitSpawn();
        Item.disappear += SpawnSingleItem;
        PlayerInventory.SpawnNewItem += SpawnSingleItem;
    }

    void OnDisable()
    {
        PlayerInventory.SpawnNewItem -= SpawnSingleItem;
    }

    void SpawnArea()
    {
        maxSpawnPos.x = collider.bounds.size.x / 2;
        maxSpawnPos.y = collider.bounds.size.z / 2;
        minSpawnPos.x = -collider.bounds.size.x / 2;
        minSpawnPos.y = -collider.bounds.size.z / 2;
    }

    void InitSpawn()
    {
        for(int i = 0; i < totalSpawn; i++)
        {
            SpawnSingleItem();
        }

    }

    void SpawnSingleItem()
    {
        ItemData itemData = GameData.instance.listItem[Random.Range(0, GameData.instance.listItem.Count)];
        Transform model = PoolKit.GetPool("MAIN").Spawn(itemData.itemName);
        Transform item = PoolKit.GetPool("MAIN").Spawn("Item");
        item.transform.position = FindSpawnPos();

        int level = Random.Range(1, 4);
        I_Style style = itemData.GetRandomVisual().style;
        ItemInstance newInstance = new ItemInstance(itemData, level, style);

        item.GetComponent<Item>().Init(newInstance, newInstance.GetMat(), model);
    }

    Vector3 FindSpawnPos()
    {
        float x = Random.Range(minSpawnPos.x, maxSpawnPos.x);
        float z = Random.Range(minSpawnPos.y, maxSpawnPos.y);
        return new Vector3(x, transform.position.y, z);
        
    }

    // Vector3 FindSpawnPos()
    // {
    //     for(int i = 0; i < tryCount; i++)
    //     {
    //         float x = Random.Range(minSpawnPos.x, maxSpawnPos.x);
    //         float z = Random.Range(minSpawnPos.y, maxSpawnPos.y);
    //         Vector3 randomPos = new Vector3(x, transform.position.y, z);
    //         if(!CheckNearestTarget(randomPos)) continue;

    //         RaycastHit hitGround;
    //         if(Physics.Raycast(randomPos, Vector3.down, out hitGround, rayCastLength, groundLayer))
    //         {
    //             Debug.DrawLine(randomPos, new Vector3(randomPos.x, randomPos.y - rayCastLength, randomPos.z), Color.red, 2f);
    //             if(hitGround.collider != null)
    //             {
    //                 spawnPos.Add(new Vector3(hitGround.point.x, transform.position.y, hitGround.point.z));
    //                 return hitGround.point;
    //             }
    //         }
    //     }
    //     return Vector3.zero;
    // }

    // bool CheckNearestTarget(Vector3 target)
    // {
    //     foreach(Vector3 pos in spawnPos)
    //     {
    //         if(Vector3.Distance(target, pos) < distance)
    //             return false;
    //     }
    //     return true;
    // }

}
