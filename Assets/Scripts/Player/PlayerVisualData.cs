using System;
using UnityEditor.Animations;
using UnityEngine;

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