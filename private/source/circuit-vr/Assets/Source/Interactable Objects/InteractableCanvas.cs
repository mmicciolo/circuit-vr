using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableCanvas : MonoBehaviour {
    GameObject interactable;
    public Image background;
    public Text info;

    // Use this for initialization
    void Start () {
        //background.GetComponent<RectTransform>().anchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        //background.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
        background.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        background.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        Color tempColor = background.GetComponent<Image>().color;
        tempColor.a = 0.7f;
        background.GetComponent<Image>().color = tempColor;
        interactable = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (interactable != null)
        {
            info.GetComponent<Text>().text = interactable.name + "\n ESC to close" ;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelController.getInstance().TurnOffInteractableCanvas();
        }
    }

    public void SetInteractable(GameObject interactable)
    {
        this.interactable = interactable;
    }
}
