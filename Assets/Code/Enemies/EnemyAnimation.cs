using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] AnimationCurve yMovementCurve;

    Tween tween;

    void OnEnable()
    {
        tween = transform.DOLocalMoveY(0.5f, 1)
            .SetLoops(-1)
            .SetEase(yMovementCurve);
    }

    private void OnDisable()
    {
        tween.Kill(true);
    }
}
