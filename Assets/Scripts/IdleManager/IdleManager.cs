using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleManager : MonoBehaviour
{
    private List<IdleSlot> idleSlots = new List<IdleSlot>();

    void Start()
    {
        foreach(Transform child in transform)
            idleSlots.Add(child.GetComponent<IdleSlot>());
        LoadData();
    }

#region Save/Load
    void SaveData()
    {
        string str = JsonUtility.ToJson(idleSlots);
        PlayerPrefs.SetString("IdleManager", str);
        PlayerPrefs.Save();
    }

    void LoadData()
    {
        string str = PlayerPrefs.GetString("IdleManager");
        if(!string.IsNullOrEmpty(str))
            idleSlots = JsonUtility.FromJson<List<IdleSlot>>(str);
    }

    void OnApplicationQuit()
    {
        SaveData();
    }
#endregion
}
