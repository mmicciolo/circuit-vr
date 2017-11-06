using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour {

    public Animator animator;

    bool needsOpen;
    bool isOpen;
    bool canPlayAnimation;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        isOpen = false;
        needsOpen = false;
        canPlayAnimation = true;
    }

	// Update is called once per frame
	void Update () {
        if ((isOpen == true) && (canPlayAnimation == true))
        {
            canPlayAnimation = false;
            animator.Play("DoorOpen");
            Debug.Log("door opening");
            StartCoroutine(WaitForDoorAnimation());
        }
        if ((isOpen == false) && (canPlayAnimation == true))
        {
            canPlayAnimation = false;
            animator.Play("DoorClose");
            Debug.Log("door closing");
            StartCoroutine(WaitForDoorAnimation());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");

        isOpen = true;

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exited");

        isOpen = false;
    }

    IEnumerator WaitForDoorAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        Debug.Log("Coroutine created");
        canPlayAnimation = true;

    }
}
