using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Cam Settings")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] private Transform orientation;

    private float xRotation;
    private float yRotation;

    private float mouseInputX;
    private float mouseInputY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GetMouseInput();
        Rotate();
    }

    private void Rotate()
    {
        yRotation += mouseInputX;
        xRotation -= mouseInputY;
        xRotation = Mathf.Clamp(xRotation, -45f, 78f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void GetMouseInput()
    {
        mouseInputX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        mouseInputY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
    }
}
