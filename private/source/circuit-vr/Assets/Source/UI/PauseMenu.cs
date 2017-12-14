using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FMODUnity.RuntimeManager.GetBus("bus:/").setPaused(true);
    }
	
	// Update is called once per frame
	void Update () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        FMODUnity.RuntimeManager.GetBus("bus:/").setPaused(false);
        LevelController.getInstance().isPaused = false;
        LevelController.getInstance().pauseMenuOpen = false;
        LevelController.getInstance().closePuzzle("PauseMenu");
    }

    public void Restart()
    {
        LevelController.getInstance().Restart();
        SceneManager.LoadScene("Mitch_sDreamEnvironment");
    }

    public void Settings()
    {
        LevelController.getInstance().isPaused = true;
        LevelController.getInstance().openPuzzle("Settings");
    }

    public void Exit()
    {
        FMODUnity.RuntimeManager.GetBus("bus:/").stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        LevelController.getInstance().Restart();
        SceneManager.LoadScene("Start Screen");
    }
}
