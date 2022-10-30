using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isInputLocked = false;

    public PlayerMovement playerMovement;
    public Camera playerCamera;
    public GameObject player;

    private void Awake()
    {
        instance = this;
    }


    public Vector3 PlayerCameraPos
    {
        get
        {
            return playerCamera.transform.position;
        }
    }

    public void LockInput()
    {
        isInputLocked = true;
    }

    public void UnlockInput()
    {
        isInputLocked = false;
    }
}
