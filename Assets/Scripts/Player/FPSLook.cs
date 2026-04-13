using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FPSLook : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float smoothTime = 1f;
    private float targetInputX;
    private float targetInputY;
    private float currentInputX;
    private float currentInputY;

    void Update()
    {
        HandleInput();
    }

    void LateUpdate()
    {
        RotateCam();
    }

    void HandleInput()
    {
        if(IsPointOnObject()) return;
        if(Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase == TouchPhase.Moved)
            {
                targetInputX += t.deltaPosition.x * rotateSpeed;
                targetInputY += t.deltaPosition.y * rotateSpeed;
            }
        }
    }

    void RotateCam()
    {
        currentInputX = Mathf.Lerp(currentInputX, targetInputX, smoothTime * Time.deltaTime);
        currentInputY = Mathf.Lerp(currentInputY, targetInputY, smoothTime * Time.deltaTime);

        cam.rotation = Quaternion.Euler(currentInputY, currentInputX, 0);
    }

    bool IsPointOnObject()
    {
        if(EventSystem.current.IsPointerOverGameObject()) return true;
        if(Input.touchCount > 0)
        {
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return true;
        }
        return false;
    }
}
