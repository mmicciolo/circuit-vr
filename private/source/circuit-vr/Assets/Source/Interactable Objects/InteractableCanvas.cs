using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableCanvas : MonoBehaviour {
    GameObject interactable;
    public Image background;
    public Text info;
    GameObject objectView;


    // Use this for initialization
    void Start () {
        interactable = null;

        background.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 50);
        background.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        Color tempColor = background.GetComponent<Image>().color;
        tempColor.a = 0.85f;
        background.GetComponent<Image>().color = tempColor;

        objectView = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (interactable != null)
        {
            info.GetComponent<Text>().text = interactable.name + "\n Hold left mouse and drag to examine \n ESC to close" ;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelController.getInstance().TurnOffInteractableCanvas();
            GameObject.Destroy(objectView);
        }

    }

    public void SetInteractable(GameObject interactable)
    {
        this.interactable = interactable;
        objectView = GameObject.Instantiate(interactable, LevelController.getInstance().interactablePopUpCamera.transform);
        objectView.transform.SetAsFirstSibling();
        objectView.transform.position = new Vector3(403, 330, 0);
        objectView.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        objectView.AddComponent<RotateWithMouse>();
    }

}
