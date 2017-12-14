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
        SceneManager.LoadScene("Mitch_sDreamEnvironment");
    }

    public void SettingsButtonClicked()
    {
        //SettingsCanvas.SetActive(!SettingsCanvas.active);
        SceneManager.LoadScene("Settings");
    }

    public void ShowCredits()
    {
        gameObject.transform.Find("Credit Panel").gameObject.SetActive(true);
    }

    public void HideCredits()
    {
        gameObject.transform.Find("Credit Panel").gameObject.SetActive(false);
    }

    public void ShowStory()
    {
        gameObject.transform.Find("Game Story").gameObject.SetActive(true);
    }

    public void HideStory()
    {
        gameObject.transform.Find("Game Story").gameObject.SetActive(false);
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
