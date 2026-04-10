using System.Collections.Generic;
using HellTap.PoolKit;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] ItemScriptable[] itemScriptables;
    [SerializeField] int totalSpawn;
    private Vector2 maxSpawnPos;
    private Vector2 minSpawnPos;
    [SerializeField] float rayCastLength = 6f;
    [SerializeField] int tryCount = 10;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float distance = 5f;
    private List<Vector3> spawnPos = new List<Vector3>();
    private new Collider collider;

    [SerializeField] private Pool Pool;

    void Start()
    {
        collider = GetComponent<Collider>();
        SpawnArea();
        InitSpawn();
        Item.disappear += SpawnSingleItem;
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
        // item.position = FindSpawnPos();
        // item.rotation = Quaternion.identity;
        //item.GetComponent<Item>().Init();

        Pool = PoolKit.GetPool("MAIN");
        ItemScriptable itemData = itemScriptables[Random.Range(0, itemScriptables.Length)];
        Transform item = Pool.Spawn(itemData.itemName);
        item.transform.position = FindSpawnPos();
        ItemStyleEntry entry = new ItemStyleEntry(itemData, itemData.GetRandomVisual());
        item.GetComponent<Item>().Init(itemData.ID, itemData.itemName, itemData.GetRarity(), itemData.GetMaterial(entry.styleVisual.style));
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
