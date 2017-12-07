﻿using Assets.Source.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static LevelController levelControllerInstance = null;

    public bool isPaused;
    public Camera interactablePopUpCamera;

    InteractableCanvas interactableCanvas;
    Assets.Source.Player.FirstPersonPlayer firstPersonPlayer;

    public Vector3 playerPosition = new Vector3(6f,1f,4f);

    private static string openScene;
    private static List<GameObject> disabledObject = new List<GameObject>();

    public PuzzleController puzzleController = new PuzzleController();

	bool[] PuzzlesComplete;

    private void Awake()
    {
        //If the instance is already created then return it, if not create new instance and return
        if (levelControllerInstance == null)
        {
            levelControllerInstance = this;
            firstPersonPlayer = GameObject.FindObjectOfType<Assets.Source.Player.FirstPersonPlayer>();
            interactableCanvas = GameObject.FindObjectOfType<InteractableCanvas>();
			PuzzlesComplete = new bool[12];
			for(int i = 0; i < PuzzlesComplete.Length; i++)
			{
				PuzzlesComplete[i] = false;
			}
            DontDestroyOnLoad(levelControllerInstance);
        }
    }

	public void SetCompleted(int puzzleNumber) {
		PuzzlesComplete [puzzleNumber] = true;
	}

    // Use this for initialization
    void Start () {
        levelControllerInstance.isPaused = false;
        interactablePopUpCamera.gameObject.SetActive(false);
    }

	public bool CheckDoorCanOpen(int puzzleNum){
		return PuzzlesComplete [puzzleNum];
	}

    public void RememberPosition()
    {
        playerPosition = firstPersonPlayer.transform.position;
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

    public void openPuzzle(string sceneName)
    {
        //Store the currently open scene
        openScene = SceneManager.GetActiveScene().name;

        //Add a callback for when the loading completes
        SceneManager.sceneLoaded += OnPuzzleLoaded;

        //Load the scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void OnPuzzleLoaded(Scene scene, LoadSceneMode mode)
    {
        //Remove the callback
        SceneManager.sceneLoaded -= OnPuzzleLoaded;

        //Disable the main scene objects
        foreach (GameObject obj in SceneManager.GetSceneByName(openScene).GetRootGameObjects())
        {
            if (!obj.active) { disabledObject.Add(obj); } else { obj.SetActive(false); }
        }

        //Enable the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Set the scene as active
        SceneManager.SetActiveScene(scene);
    }

    public void closePuzzle(string name)
    {
        //Add a callback for when the unloading completes
        SceneManager.sceneUnloaded += OnPuzzleUnloaded;

        //Load the scene
        SceneManager.UnloadScene(name);
    }

    private void OnPuzzleUnloaded(Scene scene)
    {
        //Remove the callback
        SceneManager.sceneUnloaded -= OnPuzzleUnloaded;

        //Enable the main scene objects
        foreach (GameObject obj in SceneManager.GetSceneByName(openScene).GetRootGameObjects())
        {
            if (!disabledObject.Contains(obj)) { obj.SetActive(true); }
        }

        //Clear the list
        disabledObject.Clear();

        //Disable the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Set the scene as active
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(openScene));
    }
}
