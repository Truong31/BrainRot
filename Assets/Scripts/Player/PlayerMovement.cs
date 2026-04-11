using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HellTap.PoolKit;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerAnim anim;
    [Header("Movement")]
    [SerializeField] FixedJoystick joystick;
    public float speed { get; private set; } = 5f;
    public float jumpForce { get; private set; } = 5f;
    private Vector3 direction;
    private CharacterController characterController;

    [Header("Gravity and Jump")]
    private bool isGrounded = true;
    [SerializeField] private int jumpCount = 2;
    private float gravity = -9.8f;
    private float velocityY;
    private float horizontal;
    private float vertical;

    private Camera main;

    public void Init(float speed, float jumpForce)
    {
        this.speed = speed;
        this.jumpForce = jumpForce;
    }

    public void Start()
    {
        main = Camera.main;
        characterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
        Movement();
        CheckGrounded();
    }

    public void Movement()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        Vector3 camForward = main.transform.forward;
        Vector3 camRight = main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        direction = camForward * vertical + camRight * horizontal;
        anim.RunAnim(direction.magnitude);
        
        if(direction.normalized.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
        if(!isGrounded) velocityY += gravity * Time.deltaTime;
        direction = direction.normalized * speed;
        direction.y = velocityY;
        characterController.Move(direction * Time.deltaTime);
    }

    void CheckGrounded()
    {
        isGrounded = characterController.isGrounded;
        anim.StopJump(isGrounded);
        if (isGrounded && velocityY < 0)
        {
            velocityY = -2;
            jumpCount = 2;
        }
    }

    public void Jumping()
    {
        if(jumpCount > 0)
        {
            jumpCount--;
            anim.PlayJumpAnim();
            velocityY = jumpForce;
        }
    }

}