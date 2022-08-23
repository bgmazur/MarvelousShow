using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    This is the scene for choosing minigame, will be played in between games!
*/
public class SelectMinigameManager : MonoBehaviour
{
    [SerializeField]
    private CanvasRecaller recaller;

    [SerializeField]
    private WheelOfFortuneManager wheelOfFortuneManager;

    private List<string> initialTexts = new List<string>() {
        "This is where your stars align and your fate is decided.",
        "That is, a Challenge is being selected!",
        "As you can see, completing any given Challenge once does not guarantee you won't be faced with the same Challenge again!",
        "It's wheel after all!",
        "But I can see the Audience is already falling asleep.",
        "They need entertainment!",
        "That's what we're here for, right, Contestant?",
        "Without further ado, then!",
        "Let's spin the wheel!",
    };

    private List<string> laterTexts = new List<string>() {
        "Last one was entertaining in particular!",
        "Okay, let's see what future holds for you for next Challenge!"
    };

    private List<string> laterTexts2 = new List<string>() {
        "I am very much enjoying this!",
        "Let us spin once again!"
    };

    private List<string> afterFirstChallenge = new List<string>() {
        "And how was it? Was it rough?",
        "You know the rules now!",
        "Onto your second challenge we spin!"
    };

    private List<string> beforeLastChallenge = new List<string>() {
        "This is it! Your last Challenge!",
        "Good ol' Wheel, let's make this one the most entertaining!",
    };
    private void OnEnable() {
        CanvasManager.DialogueEnded += ReactToEndOfDialogue;    
    }

    private void OnDisable() {
        CanvasManager.DialogueEnded -= ReactToEndOfDialogue;    
    }

    void Start()
    {
        var canvas = recaller.GetCanvasManager();
        List<int> poses;
        switch (canvas.progress) {
            case 0:
                poses = new List<int>() {0, 2, 2, 2, 1, 0, 2, 0, 2};
                canvas.InitializeDialogue(initialTexts, poses);
                break;
            case 1:
                poses = new List<int>() {0, 1, 2};
                canvas.InitializeDialogue(afterFirstChallenge, poses);
                break;
            case 2:
                poses = new List<int>() {0, 2};
                canvas.InitializeDialogue(laterTexts, poses);
                break;
            case 3:
                poses = new List<int>() {0, 2};
                canvas.InitializeDialogue(laterTexts2, poses);
                break;
            case 4:
                poses = new List<int>() {0, 2};
                canvas.InitializeDialogue(beforeLastChallenge, poses);
                break;
        }
        GameObject host = canvas.transform.GetChild(0).gameObject;
        PrepareHost(host);
    }

    private void PrepareHost(GameObject host) {
        RectTransform rect = host.transform as RectTransform;
        rect.anchoredPosition = new Vector2(-576, -77);
        rect.localRotation = Quaternion.Euler(0, 180, 0);
        rect.localScale = new Vector3(90, 90, 90);
    }

    private void ReactToEndOfDialogue() {
        wheelOfFortuneManager.Spin();
        // todo return the result, wait for animation to end and load the minigame
        StartCoroutine(LoadMinigame());
    }

    private IEnumerator LoadMinigame() {
        yield return new WaitForSeconds(5.0f);
        int minigame = Random.Range(0, 3);
        switch (minigame) {
            case 0:
                SceneManager.LoadScene("WireMinigame");
                break;
            case 1:
                SceneManager.LoadScene("TriviaMinigame");
                break;
            case 2:
                SceneManager.LoadScene("MazeMinigame");
                break;
        }
    }
}
