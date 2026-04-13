using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable/Player")]
public class PlayerScriptable : ScriptableObject
{
    public List<PlayerVisualData> listVisual = new List<PlayerVisualData>();
}

[Serializable]
public class PlayerVisualData
{
    public VisualType visualType;
    public GameObject visualModel;
    public AnimatorController anim;
}

public enum VisualType
{
    Visual1,
    Visual2,
    Visual3
}
