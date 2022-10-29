using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    AudioSource audioSource;
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();

        audioSource.Play();
        audioSource.Pause();
    }

    private void Update()
    {
        if (playerMovement.IsPlayerMoving)
        {
            audioSource.UnPause();
        } else
        {
            audioSource.Pause();
        }
    }
}
