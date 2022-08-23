using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MazeMinigameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timer;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform end;

    [SerializeField]
    private CanvasRecaller recaller;

    [SerializeField, Range(10, 90)]
    private float timeToCompleteInSeconds;

    private float currentTime;

    [SerializeField, Range(1f, 10f)]
    private float movementSpeed = 5f;

    public List<GameObject> tileMapsList;

    private void Start() {
        currentTime = timeToCompleteInSeconds;
        recaller.GetCanvasManager().HideDialogue();

        setMap();
    }

    private void Update() {
        currentTime -= Time.deltaTime;
        timer.text = currentTime.ToString("F");

        if (currentTime < 0.0f) {
            recaller.GetCanvasManager().LoseMinigame();
            Debug.Log("lose");
            SceneManager.LoadScene("SelectMinigame");
        }

        if (Vector2.Distance(player.localPosition, end.localPosition) < 0.5f) {
            recaller.GetCanvasManager().WinMinigame();
            Debug.Log("win");
            SceneManager.LoadScene("SelectMinigame");
        }
    }

    private void setMap()
    {
        int rnd = Random.Range(0, tileMapsList.Count);
        Debug.Log(rnd);

        tileMapsList[rnd].SetActive(true);
    }
}
