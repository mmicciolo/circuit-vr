using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour {

    public Animator animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        //animation.Play("DoorOpen");
        gameObject.GetComponent<Animator>().Play("DoorOpen");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exited");
        //animator.Play("DoorClose");
        gameObject.GetComponent<Animator>().Play("DoorClose");
    }

}
