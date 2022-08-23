using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

/*
    Responsible for 'randomly' choosing minigames
*/
public class WheelOfFortuneManager : MonoBehaviour
{
    [SerializeField, Range(10f, 1000f)]
    private float initialSpeed = 5f;

    [SerializeField, Range(0f, 100f)]
    private float decreasingSpeedFactor = 0.1f;

    private float currentSpeed = -1f;

   void Update()
    {
        if (IsSpinning()) {
            transform.Rotate(Vector3.forward, currentSpeed * Time.deltaTime, Space.Self);
            currentSpeed -= decreasingSpeedFactor * Time.deltaTime;
        }
        
    }

    private bool IsSpinning() {
        return currentSpeed > 0.0f;
    }

    public void Spin() {
        currentSpeed = initialSpeed;
    }
}
