using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private static LevelController levelControllerInstance = null;

    public bool isPaused;
    public Camera interactablePopUpCamera;

    InteractableCanvas interactableCanvas;
    Assets.Source.Player.FirstPersonPlayer firstPersonPlayer;

    private void Awake()
    {
        //If the instance is already created then return it, if not create new instance and return
        if (levelControllerInstance == null)
        {
            levelControllerInstance = this;
            firstPersonPlayer = GameObject.FindObjectOfType<Assets.Source.Player.FirstPersonPlayer>();
            interactableCanvas = GameObject.FindObjectOfType<InteractableCanvas>();
        }
    }

    // Use this for initialization
    void Start () {
        levelControllerInstance.isPaused = false;
        interactablePopUpCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {

    }

    private void Pause()
    {
        isPaused = !isPaused;
    }

    public void ShowInteractableCanvas(GameObject interactableObject)
    {
        Pause();
        Time.timeScale = 0;
        interactablePopUpCamera.gameObject.SetActive(true);
        interactableCanvas.SetInteractable(interactableObject);
    }

    public void TurnOffInteractableCanvas()
    {
        Pause();
        Time.timeScale = 1;
        interactablePopUpCamera.gameObject.SetActive(false);
    }

    private LevelController()
    {
    }

    public static LevelController getInstance()
    {
        return levelControllerInstance;
    }
}
