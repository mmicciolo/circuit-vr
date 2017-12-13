using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour {
    bool rotating;
    // Use this for initialization
    void Start () {
        rotating = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (rotating)
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.unscaledDeltaTime * 50);
        }
    }


    private void OnMouseDown()
    {
        rotating = true;
    }

    private void OnMouseUp()
    {
        rotating = false;
    }
}
