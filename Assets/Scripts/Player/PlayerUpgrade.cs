using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField] float speedIncrease = 3;
    [SerializeField] float jumpForceIncrease = 2;

    public void UpgradeSpeed()
    {
        //speed += speedIncrease;
    }

    public void UpgradeJumpforce()
    {
        //jumpForce += jumpForceIncrease;
    }
}
