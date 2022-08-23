using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    This will be played only once, only at the beginning, introductory scene
*/
public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private CanvasManager canvasManager;

    private readonly List<string> initialTexts = new List<string>() {
        "Hello and welcome to Marvelous Show!", 
        "Tonight I, Puppet Marvel, will be your host!", 
        "I'm sure the Audience already knows the rules, but before starting the show we have to remind them for our Contestant.", 
        "You know, rules our rules after all!", 
        "You'll be faced with all sort of Challenges tonight!", 
        "To get through alive you can only fail two Challenges!", 
        "When you lose your third challenge, well, let's say it will leave you breathless.", 
        "This is indicated by the Hearts in top right!", 
        "Now, to the left side!", 
        "You see here five dark circles.", 
        "This is what we call Progress in Marvelous Show!", 
        "If you complete Challenge in any way, winning or losing, a circle from Progress will light up!", 
        "As you can see, you need to complete five Challenges to be over with it!", 
        "Now! Let's move onto our beloved Fortune Wheel of Fortune!", 
    };

    private readonly List<int> poses = new List<int>() {
        0,
        2,
        1,
        1,
        0,
        2,
        2,
        1,
        0,
        0,
        2,
        2,
        0,
        2
    };

    private void OnEnable() {
        CanvasManager.DialogueEnded += ReactToEndOfDialogue;    
    }

    private void OnDisable() {
        CanvasManager.DialogueEnded -= ReactToEndOfDialogue;    
    }

    void Start()
    {
        canvasManager.InitializeDialogue(initialTexts, poses);
    }

    private void ReactToEndOfDialogue() {
        SceneManager.LoadScene("SelectMinigame");
    }
}
