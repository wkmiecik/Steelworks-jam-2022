using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform portalPlane;
    [SerializeField] Collider endTrigger;
    [SerializeField] Volume localVolume;

    Vector3 startScale;

    private void Start()
    {
        startScale = portalPlane.localScale;
        portalPlane.DOScale(Vector3.zero, 1);

        endTrigger.enabled = false;
        localVolume.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
            Debug.Log("GAME END");
        }
    }

    public void Open()
    {
        portalPlane.DOScale(startScale, 2);

        endTrigger.enabled = true;
        localVolume.enabled = true;
    }
}
