using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    //public GameObject SettingsCanvas;

    // Use this for initialization
    void Start()
    {
        //SettingsCanvas.SetActive(false);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void SettingsButtonClicked()
    {
        //SettingsCanvas.SetActive(!SettingsCanvas.active);
        SceneManager.LoadScene("Settings");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
