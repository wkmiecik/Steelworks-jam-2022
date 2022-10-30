using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    EnemyAnimation model;
    BoxCollider damageTrigger;
    PlayerMovement pm;
    VisualEffect vfx;
    MeshRenderer meshRenderer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        model = GetComponentInChildren<EnemyAnimation>();
        damageTrigger = GetComponent<BoxCollider>();
        pm = FindObjectOfType<PlayerMovement>();
        vfx = GetComponentInChildren<VisualEffect>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        if (agent.isActiveAndEnabled)
            agent.SetDestination(pm.transform.position);
    }

    public void Die(Vector3 impulse)
    {
        model.tween.Kill();
        agent.enabled = false;
        damageTrigger.enabled = false;
        
        var force = impulse / Time.fixedDeltaTime;

        model.transform.DOBlendableLocalRotateBy(new Vector3(0, 0, 90), 1f);
        model.transform.DOBlendableMoveBy(Vector3.down * 2f, 1f);
        //model.transform.DOBlendableMoveBy(Vector3.forward * -1f, 1f);

        StartCoroutine(Dying());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var healthSystem = other.GetComponentInParent<healthSystem>();
            healthSystem.TakeDamege(20);
        }
    }

    private IEnumerator Dying()
    {
        yield return new WaitForSeconds(.2f);

        vfx.Play();

        yield return new WaitForSeconds(1);

        meshRenderer.enabled = false;

        yield return new WaitForSeconds(2);

        Destroy(gameObject);

        yield return 0;
    }
}
