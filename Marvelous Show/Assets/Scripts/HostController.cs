using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HostController : MonoBehaviour
{
    [SerializeField]
    private int currentPose = 0;

    [SerializeField]
    private Image[] poses;

    private readonly Color WHITE_NO_ALPHA = new Color(1f, 1f, 1f, 0f);

    public void SwitchToPose(int poseNumber)
    {
        if (poseNumber != currentPose)
        {
            DOTween.Sequence()
                .Append(poses[currentPose].DOColor(WHITE_NO_ALPHA, 0.4f).SetEase(Ease.Linear))
                .Insert(0f, poses[poseNumber].DOColor(Color.white, 0.4f).SetEase(Ease.Linear))
                .SetLink(gameObject);
            currentPose = poseNumber;
        }
    }
}
