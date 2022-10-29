using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] AnimationCurve yMovementCurve;
    [SerializeField] float animLength;

    Tween tween;

    void OnEnable()
    {
        tween = transform.DOLocalMoveY(0.1f, animLength)
            .SetLoops(-1)
            .SetEase(yMovementCurve);
    }

    void OnDisable()
    {
        tween.Kill(true);
    }
}
