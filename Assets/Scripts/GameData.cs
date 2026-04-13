using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    public static GameData instance;

    public List<ItemData> listItem;
    public PlayerScriptable playerScriptable;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            DontDestroyOnLoad(this);
    }
}
