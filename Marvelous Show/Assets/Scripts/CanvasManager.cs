using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
    It takes care of all the UI - progress bars, health bars, dialogues etc
*/
public class CanvasManager : MonoBehaviour
{
    public delegate void CanvasEvent();
    public static event CanvasEvent DialogueEnded;

    private static Color SUCCESS_PROGRESS_COLOR = Color.green;
    private static Color FAIL_PROGRESS_COLOR = Color.red;
    private static Color CURRENT_PROGRESS_COLOR = Color.yellow;

    [SerializeField]
    private TextMeshProUGUI displayedText;

    [SerializeField]
    private int currentText = -1;

    [SerializeField]
    private GameObject dialogue;

    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private GameObject progressBar;

    [SerializeField]
    private HostController host;

    private List<string> texts;
    private List<int> poses;
    private int healthPoints;
    public int progress = 0;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //MakeHeartsPulse(); todo maybe add delicate swing?
    }

    private void MakeHeartsPulse()
    {
        healthPoints = healthBar.transform.childCount;
        for (int i = 0; i < healthBar.transform.childCount; ++i)
        {
            Transform heart = healthBar.transform.GetChild(i);
            Sequence sequence = DOTween.Sequence();
            sequence
                .SetId("heart-pulse")
                .SetLink(gameObject)
                .Append(heart.DOScale(1.2f, 0.8f))
                .Append(heart.DOScale(1.0f, 0.8f))
                .SetLoops(-1);
        }
    }

    public void InitializeDialogue(List<string> texts, List<int> poses = null)
    {
        currentText = -1;
        this.texts = texts;
        NextText();
        ShowDialogue();
        this.poses = poses;
    }

    private void Update()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) && IsShowingDialogue())
        {
            NextText();
        }
    }

    private void NextText()
    {
        currentText++;
        if (currentText < texts.Count)
        {
            displayedText.text = texts[currentText];
        }
        else
        {
            DialogueEnded();
        }
        if (poses != null) {
            host.SwitchToPose(poses[currentText]);
        }
    }

    public void HideDialogue()
    {
        dialogue.SetActive(false);
        host.gameObject.SetActive(false);
    }

    public void ShowDialogue()
    {
        dialogue.SetActive(true);
        host.gameObject.SetActive(true);
    }

    public bool IsShowingDialogue()
    {
        return dialogue.activeSelf;
    }

    public void WinMinigame()
    {
        AdvanceInProgressBar(SUCCESS_PROGRESS_COLOR);
    }

    public void LoseMinigame()
    {
        LoseHeart();
        AdvanceInProgressBar(FAIL_PROGRESS_COLOR);
    }

    private void LoseHeart()
    {
        healthPoints--;
        if (healthPoints <= 0)
        {
            Debug.Log("GAME LOST - HP IS ZERO!");
        }
        else
        {
            var heartBeingLost = healthBar.transform.GetChild(healthBar.transform.childCount - 1);
            DOTween.Sequence()
                .Append(heartBeingLost.DOLocalMoveY(heartBeingLost.localPosition.y + 200f, 2f).SetEase(Ease.InBack).SetLink(heartBeingLost.gameObject))
                .AppendCallback(() => Destroy(heartBeingLost.gameObject));
        }
    }

    private void AdvanceInProgressBar(Color colorToPaintLastProgress)
    {
        var current = progressBar.transform.Find($"Bar{progress}");
        current.gameObject.SetActive(false);
        progress++;
        var next = progressBar.transform.Find($"Bar{progress}");
        next.gameObject.SetActive(true);
    }

}
