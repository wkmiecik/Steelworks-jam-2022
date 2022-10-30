using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    [SerializeField] private BoxCollider handleCollider;
    [SerializeField] private MeshCollider meshCollider;

    private void Update()
    {
        meshCollider.isTrigger = handleCollider.isTrigger;
    }
}
