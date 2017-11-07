using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private static LevelController levelControllerInstance = null;

    InteractableCanvas interactableCanvas;
    Assets.Source.Player.FirstPersonPlayer firstPersonPlayer;

    private void Awake()
    {
        //If the instance is already created then return it, if not create new instance and return
        if (levelControllerInstance == null)
        {
            levelControllerInstance = new LevelController();
            levelControllerInstance.firstPersonPlayer = GameObject.FindObjectOfType<Assets.Source.Player.FirstPersonPlayer>();
            levelControllerInstance.interactableCanvas = GameObject.FindObjectOfType<InteractableCanvas>();
            levelControllerInstance.interactableCanvas.gameObject.SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void ShowInteractableCanvas(GameObject interactableObject)
    {
        Time.timeScale = 0;
        interactableCanvas.gameObject.SetActive(true);
        //firstPersonPlayer.gameObject.;
        interactableCanvas.SetInteractable(interactableObject);
    }

    public void TurnOffInteractableCanvas()
    {
        Time.timeScale = 1;
        interactableCanvas.gameObject.SetActive(false);
        //firstPersonPlayer.gameObject.SetActive(true);
    }

    private LevelController()
    {
    }

    public static LevelController getInstance()
    {
        return levelControllerInstance;
    }
}
