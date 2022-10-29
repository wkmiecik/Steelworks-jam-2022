using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleRotation : MonoBehaviour
{
    [SerializeField] AnimationCurve rotationCurve;
    [SerializeField] float animLength;

    Tween tween;

    void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, Random.value * 360, 0);

        tween = transform.DOLocalRotate(new Vector3(0,180,0), animLength)
            .SetLoops(-1)
            .SetEase(rotationCurve);
    }

    void OnDisable()
    {
        tween.Kill(true);
    }
}
