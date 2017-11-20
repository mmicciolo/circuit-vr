using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //public GameObject SettingsCanvas;

	// Use this for initialization
	void Start () {
        //SettingsCanvas.SetActive(false);
	}
	
    public void StartGame()
    {
        SceneManager.LoadScene("SpaceShip Scaled");
    }

    public void SettingsButtonClicked()
    {
        //SettingsCanvas.SetActive(!SettingsCanvas.active);
        SceneManager.LoadScene("Settings");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
