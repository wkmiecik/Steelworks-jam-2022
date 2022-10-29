using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerMovement playerMovement;
    public Camera playerCamera;

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
}
