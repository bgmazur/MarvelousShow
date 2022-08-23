using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Example minigame
*/
public class RunningMinigameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timer;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform end;

    [SerializeField]
    private CanvasRecaller recaller;

    [SerializeField, Range(10, 25)]
    private float timeToCompleteInSeconds;

    private float currentTime;

    [SerializeField, Range(1f, 10f)]
    private float movementSpeed = 5f;

    private void Start() {
        currentTime = timeToCompleteInSeconds;
        recaller.GetCanvasManager().HideDialogue();
    }

    private void Update() {
        currentTime -= Time.deltaTime;
        timer.text = currentTime.ToString("F");

        if (currentTime < 0.0f) {
            recaller.GetCanvasManager().LoseMinigame();
            Debug.Log("lose");
            SceneManager.LoadScene("SelectMinigame");
        }

        if (Vector2.Distance(player.localPosition, end.localPosition) < 1.0f) {
            recaller.GetCanvasManager().WinMinigame();
            Debug.Log("win");
            SceneManager.LoadScene("SelectMinigame");
        }

        float verticalMovement = Input.GetAxis("Horizontal");
        player.localPosition += new Vector3(verticalMovement * movementSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

}
