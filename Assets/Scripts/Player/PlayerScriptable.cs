using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable/Player")]
public class PlayerScriptable : ScriptableObject
{
    public List<PlayerVisualData> listVisual = new List<PlayerVisualData>();
}
