using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TVManager : MonoBehaviour
{
    [SerializeField]
    private float endOrthoSize = 2.8f;

    [SerializeField]
    private Vector3 cameraEndPosition = new Vector3(0f, 0.7f, 0f);

    [SerializeField]
    private float duration = 2f;

    void Start()
    {
        DOTween.Sequence()
        .Append(Camera.main.DOOrthoSize(endOrthoSize, duration).SetEase(Ease.InCubic))
        .Insert(0, Camera.main.transform.DOLocalMove(cameraEndPosition, duration).SetEase(Ease.InCubic))
        .AppendCallback(() => SceneManager.LoadScene("Main", LoadSceneMode.Single))
        .SetLink(gameObject);
    }
}
