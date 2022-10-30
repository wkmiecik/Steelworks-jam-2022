using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadCurrentScene : MonoBehaviour
{
    [SerializeField] private HealthSystem playerHealthSystem;
    private Animator animator;
    private AudioSource playerDeathAudio;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerDeathAudio = GetComponent<AudioSource>();
        playerHealthSystem.OnDied += PlayerHealthSystem_OnDied;
    }

    public void OnFadeEnd()
    {
        ReloadLevel();
    }

    private void PlayerHealthSystem_OnDied(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Fadeout");
        playerDeathAudio.Play();
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
