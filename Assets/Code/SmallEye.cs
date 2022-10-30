using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SmallEye : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject eyeBroken;
    [SerializeField] private GameObject eyeFine;
    [SerializeField] private Rigidbody[] pieces;

    private bool isLooking = true;

    private void Update()
    {
        if (isLooking)
        {
            transform.LookAt(player);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bullet>() != null)
        {
            DestoryEye();
        }
    }

    private void DestoryEye()
    {
        isLooking = false;
        SmallEyeManager.Instance.EyeDestroyed();
        eyeBroken.SetActive(true);
        eyeFine.SetActive(false);
        DestroyPieces();
    }

    private void DestroyPieces()
    {
        foreach (var piece in pieces)
        {
            float random = Random.Range(10f, 15f); 
            Destroy(piece.gameObject, random);
        }
        Destroy(this, 20f);
    }
}
