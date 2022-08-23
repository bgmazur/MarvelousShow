using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriviaMinigameManager : MonoBehaviour
{
    [SerializeField]
    private CanvasRecaller recaller;

    [SerializeField]
    private TextMeshProUGUI timer;

    [SerializeField, Range(10, 25)]
    private float timeToCompleteInSeconds = 10f;

    [SerializeField]
    private GameObject triviaBanner;
    [SerializeField]
    private GameObject button1;
    [SerializeField]
    private GameObject button2;
    [SerializeField]
    private GameObject button3;
    [SerializeField]
    private TextMeshProUGUI question;
    [SerializeField]
    private Transform leftWall;
    [SerializeField]
    private Transform rightWall;
    [SerializeField]
    private Transform endWall;
    private int triviaProgress = 0;
    [SerializeField, Range(0.5f, 2f)]
    private float wallsPositionDelta = 1.5f;
    private int triviaHealth = 3;

    private float currentTime;
    private bool isTimerCountingDown = false;

    private readonly List<TriviaQuestion> triviaQuestions = new List<TriviaQuestion>()
    {
        new TriviaQuestion("Which door Stanley in Stanley's Parable picked!", new string[] { "Left", "Who's Stanley?", "Right" }, 0),
        new TriviaQuestion("Which island Guybrush Threepwood visits in LucasArts adventure games!", new string[] { "Banana Island", "Monkey Island", "Pirate Island" }, 1),
        new TriviaQuestion("Which castle in Heroes of Might and Magic series can produce imps?", new string[] { "Inferno", "Necropolis", "Fortress" }, 0),
        new TriviaQuestion("Which castle in Heroes of Might and Magic series can produce vampires?", new string[] { "Inferno", "Necropolis", "Fortress" }, 1),
        new TriviaQuestion("Which spell opens locks in Harry Potter series", new string[] { "Alakazam", "Alohomora", "Rictusempra" }, 1),
        new TriviaQuestion("What is the name of Geralt the Witcher horse", new string[] { "Cockroach", "Pegaz", "Roach" }, 2),
        new TriviaQuestion("What is the female pop singer champion in League of Legends released in November 2020", new string[] { "Samira", "Seraphine", "Yasuo damn it" }, 1),
        new TriviaQuestion("When Mario was created?", new string[] { "1980s", "1990s", "Mario predates humanity" }, 0),
        new TriviaQuestion("Metin?", new string[] { "Metin3", "Metin2", "Trivago" }, 1),
        new TriviaQuestion("Which elves are NOT present in Warcraft universe", new string[] { "Blood elves", "Night elves", "Nature elves" }, 2),
    };

    private void Start()
    {
        recaller.GetCanvasManager().HideDialogue();
        Reset();
    }

    private void SetupQuestionVisuals(TriviaQuestion triviaQuestion)
    {
        question.text = triviaQuestion.question;
        var buttons = new GameObject[] { button1, button2, button3 };
        for (int i = 0; i < buttons.Length; ++i)
        {
            var button = buttons[i];
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = triviaQuestion.answers[i];
            var onClick = button.GetComponent<Button>().onClick;
            if (triviaQuestion.correctAnswer == i)
            {
                onClick.RemoveAllListeners();
                onClick.AddListener(Win);
            }
            else
            {
                onClick.RemoveAllListeners();
                onClick.AddListener(Lose);
            }
        }
    }

    private void Update()
    {
        if (isTimerCountingDown)
        {
            currentTime -= Time.deltaTime;
            timer.text = currentTime.ToString("F");

            if (currentTime < 0.0f)
            {
                Lose();
            }
        }

    }

    private void Lose()
    {
        Debug.Log("Lost");
        ++triviaProgress;
        --triviaHealth;
        StopTimer();
        var duration = 2f;
        var cameraShake = Camera.main.DOShakePosition(duration, 0.05f, 10, 90, false);
        var rightWallMovement = rightWall.DOLocalMoveX(rightWall.localPosition.x - wallsPositionDelta, duration).SetEase(Ease.Linear);
        var leftWallMovement = leftWall.DOLocalMoveX(leftWall.localPosition.x + wallsPositionDelta, duration).SetEase(Ease.Linear);

        var baseLoseSequence = DOTween.Sequence()
            .SetLink(gameObject)
            .AppendCallback(() => triviaBanner.SetActive(false))
            .AppendCallback(() => timer.gameObject.SetActive(false))
            .Append(cameraShake)
            .Insert(0f, rightWallMovement)
            .Insert(0f, leftWallMovement);

        if (triviaHealth > 0)
        {
            baseLoseSequence
                .AppendCallback(() => timer.gameObject.SetActive(true))
                .AppendCallback(() => triviaBanner.SetActive(true))
                .AppendCallback(Reset);
        }
        else
        {
            var deathShakeDuration = 0.5f;
            var deathShake = Camera.main.DOShakePosition(deathShakeDuration, 0.6f, 90, 90, true);
            baseLoseSequence
                .AppendCallback(ShowEndWall)
                .Append(deathShake)
                .InsertCallback(duration + deathShakeDuration + 2.0f, LoseDefinitely);
        }
    }

    private void ShowEndWall()
    {
        endWall.gameObject.SetActive(true);
    }

    private void LoseDefinitely()
    {
        recaller.GetCanvasManager().LoseMinigame();
        SceneManager.LoadScene("SelectMinigame");
    }

    private void Win()
    {
        ++triviaProgress;
        if (triviaProgress == 2)
        {
            recaller.GetCanvasManager().WinMinigame();
            SceneManager.LoadScene("SelectMinigame");
        }
        else
        {
            Reset();
        }
    }

    private void Reset()
    {
        SetupQuestion();
        currentTime = timeToCompleteInSeconds;
        StartTimer();
    }

    private void SetupQuestion()
    {
        var questionNumber = Random.Range(0, triviaQuestions.Count);
        var triviaQuestion = triviaQuestions[questionNumber];
        SetupQuestionVisuals(triviaQuestion);
        triviaQuestions.Remove(triviaQuestion);
    }

    private void StopTimer()
    {
        isTimerCountingDown = false;
    }

    private void StartTimer()
    {
        isTimerCountingDown = true;
    }
}

class TriviaQuestion
{
    public string question;
    public string[] answers;
    public int correctAnswer;

    public TriviaQuestion(string question, string[] answers, int correctAnswer)
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswer = correctAnswer;
    }
}