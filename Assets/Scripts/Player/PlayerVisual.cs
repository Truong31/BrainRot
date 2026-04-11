using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private PlayerAnim playerAnim;
    [SerializeField] private PlayerScriptable playerScriptable;
    private GameObject visual;
    private Animator anim;
    public VisualType visualType { get; private set; }

    void Start()
    {
        ChangeVisual(visualType.ToString());
    }

    public void Init(VisualType visualType)
    {
        this.visualType = visualType;
    }
    
    public void ChangeVisual(string str)
    {
        VisualType type = (VisualType)Enum.Parse(typeof(VisualType), str);
        visualType = type;
        foreach(PlayerVisualData data in playerScriptable.listVisual)
        {
            if(type == data.visualType)
            {
                Destroy(visual);
                visual = Instantiate(data.visualModel, Vector3.zero, Quaternion.identity, transform);
                visual.transform.localPosition = Vector3.zero;
                visual.transform.localRotation = Quaternion.identity;
                anim = visual.AddComponent<Animator>();
                anim.runtimeAnimatorController = data.anim;
                playerAnim.Init(anim);
                break;
            }
        }
    }
}
