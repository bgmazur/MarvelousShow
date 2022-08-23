using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class WireMinigameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timer;
    [SerializeField]
    private Transform wiresParent;
    private List<WireController> wires = new List<WireController>();

    [SerializeField, Range(10, 120)]
    private float timeToCompleteInSeconds;
    [SerializeField]
    private CanvasRecaller recaller;

    private float currentTime;

    private List<string> initialTexts = new List<string>() {
        "Latest trend in games!",
        "I've spiced it up a little for you, my Contestant!",
        "Use your mouse to connect all the wires!",
        "Ready, Set, ...",
        "GO!"
    };

    private List<int> poses = new List<int>() {
        0, 2, 1, 1, 2
    };
    private bool isRunning = false;

    private void Start()
    {
        for (int i = 0; i < wiresParent.childCount; ++i)
        {
            var wireController = wiresParent.GetChild(i).gameObject.GetComponent<WireController>();
            wireController.enabled = false;
            wires.Add(wireController);
        }
        currentTime = timeToCompleteInSeconds;
        recaller.GetCanvasManager().ShowDialogue();
        recaller.GetCanvasManager().InitializeDialogue(initialTexts, poses);
        PrepareHost(recaller.GetCanvasManager().transform.GetChild(0).gameObject);
    }

    private void PrepareHost(GameObject host)
    {
        RectTransform rect = host.transform as RectTransform;
        rect.anchoredPosition = new Vector2(61, -71);
        rect.localRotation = Quaternion.Euler(0, 0, 0);
        rect.localScale = new Vector3(90, 90, 90);
    }
    
    private void OnEnable() {
        CanvasManager.DialogueEnded += ReactToEndOfDialogue;
    }

    private void OnDisable() {
        CanvasManager.DialogueEnded -= ReactToEndOfDialogue;        
    }

    private void ReactToEndOfDialogue() {
        recaller.GetCanvasManager().HideDialogue();
        isRunning = true;
        foreach(var wireController in wires) {
            wireController.enabled = true;
        }
    }


    private void Update()
    {

        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            timer.text = currentTime.ToString("F");

            if (currentTime < 0.0f)
            {
                recaller.GetCanvasManager().LoseMinigame();
                SceneManager.LoadScene("SelectMinigame");
            }

            if (wires.All(w => w.connected))
            {
                recaller.GetCanvasManager().WinMinigame();
                SceneManager.LoadScene("SelectMinigame");
            }

        }
    }
}
