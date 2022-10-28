using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float sensitivity;
    [SerializeField] Transform arms;
    [SerializeField] Transform body;

    public bool cursorLocked;

    float xRotation;

    private void Start()
    {
        LockCursor();
    }

    void Update()
    {
        MouseLookUpdate();
    }

    void MouseLookUpdate()
    {
        var mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        arms.localRotation = Quaternion.Euler(new Vector3(xRotation, 0, 0));
        body.Rotate(new Vector3(0, mouseX, 0));
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cursorLocked = true;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        cursorLocked = false;
    }
}
