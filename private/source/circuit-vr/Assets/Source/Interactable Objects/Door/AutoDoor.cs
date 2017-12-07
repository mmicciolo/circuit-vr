using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour {

    public Animator animator;

    bool needsOpen;
    bool isOpen;
	bool canOpen;
    public int puzzleNumber;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        isOpen = false;
        needsOpen = false;
    }

	// Update is called once per frame
	void Update () {
		canOpen = LevelController.getInstance ().CheckDoorCanOpen (puzzleNumber);
        if (needsOpen != isOpen)
        {
            StartCoroutine(needsOpen? OpenDoor() : CloseDoor());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
		if (canOpen) {
			Debug.Log ("entered");

			needsOpen = true;
		}
    }

    private void OnTriggerExit(Collider other)
    {
		if (canOpen) {
			Debug.Log ("exited");

			needsOpen = false;
		}
    }

    IEnumerator OpenDoor()
    {
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }
            Debug.Log("Coroutine open");
            animator.Play("DoorOpen");
        GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();
            isOpen = true;
    }

    IEnumerator CloseDoor()
    {
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }
            Debug.Log("Coroutine close");
            animator.Play("DoorClose");
        GetComponents<FMODUnity.StudioEventEmitter>()[1].Play();

        isOpen = false;
    }
}
