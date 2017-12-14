using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    [SerializeField]
    GameObject[] panels;
    

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGameStoryActive(bool active)
    {
        panels[0].SetActive(active);
    }

    public void SetCreditActive(bool active)
    {
        panels[1].SetActive(active);
    }

    public void SetSettingActive(bool active)
    {
        panels[2].SetActive(active);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = GameObject.Find("Slider Horizontal").GetComponent<UnityEngine.UI.Slider>().value;
    }

    public void Mute()
    {
        AudioListener.volume = (AudioListener.volume <= 0) ? GameObject.Find("Slider Horizontal").GetComponent<UnityEngine.UI.Slider>().value : 0;
    }
}
