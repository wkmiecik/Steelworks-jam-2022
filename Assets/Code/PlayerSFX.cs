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
<<<<<<< .merge_file_a17412
        Debug.Log(playerMovement.IsPlayerMoving);
=======
>>>>>>> .merge_file_a04116
        if (playerMovement.IsPlayerMoving)
        {
            audioSource.UnPause();
        } else
        {
            audioSource.Pause();
        }
    }
}
