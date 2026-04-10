using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;

    public void Init(Animator anim)
    {
        this.anim = anim;
    }

    public void RunAnim(float value)
    {
        if (anim == null) return;
        anim.SetFloat("Speed", value);
    }

    public void PlayJumpAnim()
    {
        if (anim == null) return;
        anim.CrossFade("Jump", 0.1f);
    }

    public void StopJump(bool value)
    {
        if (anim == null) return;
        anim.SetBool("IsGround", value);
    }

}
