using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Should be included in every minigame and used to access CanvasManager
*/
public class CanvasRecaller : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasPrefab;

    private CanvasManager canvasManager;

    private void Awake()
    {
        var canvas = GameObject.Find("Canvas");
        if (canvas) {
            canvasManager = canvas.GetComponent<CanvasManager>();
        } else {
            var canvasGameObject = Instantiate(canvasPrefab);
            canvasGameObject.name = "Canvas";
            canvasManager = canvasGameObject.GetComponent<CanvasManager>();
        }
    }

    public CanvasManager GetCanvasManager() {
        return canvasManager;
    }
}
