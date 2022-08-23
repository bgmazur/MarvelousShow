using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateLikeOnTheWind : MonoBehaviour
{

    [SerializeField, Range(0f, 40f)]
    private float maxRotation = 1.8f;

    [SerializeField, Range(1f, 10f)]
    private float rotationDuration = 4f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * -maxRotation);
        RectTransform rect = transform as RectTransform;
        DOTween.Sequence()
            .Append(rect.DOLocalRotate(Vector3.forward * maxRotation, rotationDuration, RotateMode.Fast).SetEase(Ease.Linear))
            .Append(rect.DOLocalRotate(Vector3.forward * -maxRotation, rotationDuration, RotateMode.Fast).SetEase(Ease.Linear))
            .SetLoops(-1)
            .SetLink(gameObject);
    }
}
