using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEyeManager : MonoBehaviour
{
    public event EventHandler OnEyesDestroyed;

    public static SmallEyeManager Instance { get; private set; }

    [SerializeField] private int eyes = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void EyeDestroyed()
    {
        eyes--;
        if(eyes == 0)
        {
            SomethingHappens();
        }
    }

    private void SomethingHappens()
    {
        Debug.Log("guwno");
        OnEyesDestroyed?.Invoke(this, EventArgs.Empty);
    }

}
