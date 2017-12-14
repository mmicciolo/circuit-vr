using Assets.Source.UI.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    //public GameObject SettingsCanvas;
    private float originalVolume;
    private bool originalMuted;

    private float newVolume;
    private bool newMuted;

    // Use this for initialization
    void Start()
    {
        //SettingsCanvas.SetActive(false);
        originalVolume = GlobalSettings.Instance.volume;
        originalMuted = GlobalSettings.Instance.muted;
        GetComponentInChildren<Slider>().value = originalVolume;
        if(originalMuted)
        {
            GameObject.Find("Toggle 2 Enabled").GetComponent<Toggle>().isOn = true;
            GameObject.Find("Toggle 1 Disabled").GetComponent<Toggle>().isOn = false;
        }
        else
        {
            GameObject.Find("Toggle 2 Enabled").GetComponent<Toggle>().isOn = false;
            GameObject.Find("Toggle 1 Disabled").GetComponent<Toggle>().isOn = true;
        }
    }

    public void VolumeChanged()
    {
        newVolume = GetComponentInChildren<Slider>().value;
        //FMODUnity.RuntimeManager.GetBus("bus:/").setVolume(value);
    }

    public void MuteDisabled()
    {
        if(GameObject.Find("Toggle 2 Enabled").GetComponent<Toggle>().isOn)
        {
            newMuted = true;
            //FMODUnity.RuntimeManager.GetBus("bus:/").setVolume(0);
            //Debug.Log("Muting Audio");
        }
    }

    public void MuteEnabled()
    {
        if (GameObject.Find("Toggle 1 Disabled").GetComponent<Toggle>().isOn)
        {
            newMuted = false;
            //VolumeChanged();
            //Debug.Log("Umuting Audio");
        }
    }

    public void BackToMain()
    {
        GlobalSettings.Instance.volume = newVolume;
        GlobalSettings.Instance.muted = newMuted;
        if (newMuted) { FMODUnity.RuntimeManager.GetBus("bus:/").setVolume(0); } else { FMODUnity.RuntimeManager.GetBus("bus:/").setVolume(newVolume); }
        if (LevelController.getInstance() == null) { SceneManager.LoadScene("Start Screen"); }
        else { LevelController.getInstance().closePuzzle("Settings"); }
    }

    public void RestoreSettings()
    {
        if (LevelController.getInstance() == null) { SceneManager.LoadScene("Start Screen"); }
        else { LevelController.getInstance().closePuzzle("Settings"); }
        //SceneManager.LoadScene("Start Screen");
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
