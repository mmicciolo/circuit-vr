﻿using Assets.Source.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

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

    public AutoDoor[] lockBehind;

	public string[] puzzleOrder = new string[] {"One", "K2", "Circuit6", "Circuit30", "Circuit31", "Circuit15", "Circuit29", "Circuit33", "Circuit23", "Circuit17", "Circuit34", "Circuit26" };
	public List<int> puzzlesCompleted;

	private StudioEventEmitter buzzSound;
    private Stack<string> openScenes = new Stack<string>();

    public bool pauseMenuOpen = false;

    private void Awake()
    {
        //If the instance is already created then return it, if not create new instance and return
        if (levelControllerInstance == null)
        {
            levelControllerInstance = this;
            firstPersonPlayer = GameObject.FindObjectOfType<Assets.Source.Player.FirstPersonPlayer>();
            interactableCanvas = GameObject.FindObjectOfType<InteractableCanvas>();
			puzzlesCompleted = new List<int>();
            lockBehind = new AutoDoor[10];
            int i = 0;
            foreach (GameObject doorObject in GameObject.FindGameObjectsWithTag("lock_behind")) {
                lockBehind[i] = doorObject.GetComponent<AutoDoor>();
                i++;
            }
			buzzSound = gameObject.AddComponent<StudioEventEmitter>();
			buzzSound.Event = "event:/SFX/Failed Circuit";
            DontDestroyOnLoad(levelControllerInstance);
        }
    }

    public void Restart()
    {
        Destroy(this);
    }

	public void SetCompleted(int puzzleNumber) {
		if (!puzzlesCompleted.Contains (puzzleNumber)) {
			puzzlesCompleted.Add (puzzleNumber);
		}
	}

    // Use this for initialization
    void Start () {
        levelControllerInstance.isPaused = false;
        interactablePopUpCamera.gameObject.SetActive(false);

        //Start playing the first dialog
        DialogueManager.Instance.StartNewDialog("AI Dialogue/Intro/Intro 1");

    }

	public bool CheckDoorCanOpen(int puzzleNum){
		return (puzzlesCompleted.Contains(puzzleNum));
	}

    public void RememberPosition()
    {
        playerPosition = firstPersonPlayer.transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            if (isPaused && !pauseMenuOpen)
            {
                openPuzzle("PauseMenu");
                pauseMenuOpen = true;
            } else if(!openScenes.Contains("PauseMenu"))
            {
                closePuzzle("PauseMenu");
                pauseMenuOpen = false;
            }
        }
    }

    public void Pause()
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
		//if previous puzzles are solved
		if (sceneName == puzzleOrder [puzzlesCompleted.Count]) {
            //Store the currently open scene
            //openScene = SceneManager.GetActiveScene ().name;
            openScenes.Push(SceneManager.GetActiveScene().name);

			//Add a callback for when the loading completes
			SceneManager.sceneLoaded += OnPuzzleLoaded;

			//Load the scene
			SceneManager.LoadScene (("Puzzle" + sceneName), LoadSceneMode.Additive);
		} else {
            if (sceneName == "PauseMenu")
            {
                //Store the currently open scene
                //openScene = SceneManager.GetActiveScene().name;
                openScenes.Push(SceneManager.GetActiveScene().name);

                //Add a callback for when the loading completes
                SceneManager.sceneLoaded += OnPuzzleLoaded;

                //Load the scene
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
            if (sceneName == "Settings")
            {
                //Store the currently open scene
                //openScene = SceneManager.GetActiveScene().name;
                openScenes.Push(SceneManager.GetActiveScene().name);

                //Add a callback for when the loading completes
                SceneManager.sceneLoaded += OnPuzzleLoaded;

                //Load the scene
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
            else 
			buzzSound.Play ();
		}
    }

    private void OnPuzzleLoaded(Scene scene, LoadSceneMode mode)
    {
        //Enable the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Remove the callback
        SceneManager.sceneLoaded -= OnPuzzleLoaded;

        if(openScenes.Peek().Equals("Mitch_sDreamEnvironment"))
        {
            //Disable the main scene objects
            foreach (GameObject obj in SceneManager.GetSceneByName(openScenes.Peek()).GetRootGameObjects())
            {
                if (!obj.active) { disabledObject.Add(obj); } else { obj.SetActive(false); }
            }
        }

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

        if(openScenes.Peek().Equals("Mitch_sDreamEnvironment"))
        {

            //Enable the main scene objects
            foreach (GameObject obj in SceneManager.GetSceneByName(openScenes.Peek()).GetRootGameObjects())
            {
                if (!disabledObject.Contains(obj)) { obj.SetActive(true); }
            }

            //Clear the list
            disabledObject.Clear();

            //Disable the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //Set the scene as active
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(openScenes.Pop()));
    }
}
