using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] AnimationCurve yMovementCurve;
    [SerializeField] float animLength;

    public Tween tween;

    void OnEnable()
    {
        tween = transform.DOLocalMoveY(transform.localPosition.y - 2, animLength)
            .SetLoops(-1)
            .SetEase(yMovementCurve);
    }

    void OnDisable()
    {
        tween.Kill(true);
    }
}
