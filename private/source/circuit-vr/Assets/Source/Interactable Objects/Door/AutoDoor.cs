using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour {

    public Animator animator;

    bool needsOpen;
    bool isOpen;
	bool canOpen;
    public int puzzleNumber;
    FMODUnity.StudioEventEmitter openEmitter;
    FMODUnity.StudioEventEmitter closeEmitter;

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
        isOpen = false;
        needsOpen = false;
        openEmitter = gameObject.GetComponents<FMODUnity.StudioEventEmitter>()[0];
        closeEmitter = gameObject.GetComponents<FMODUnity.StudioEventEmitter>()[1];
    }

	// Update is called once per frame
	void Update () {
        if (puzzleNumber == 20) canOpen = true;
        else
        {
            canOpen = LevelController.getInstance().CheckDoorCanOpen(puzzleNumber);
        }

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
        if (needsOpen)
        {
            Debug.Log("Coroutine open");
            animator.Play("DoorOpen");

            //FMOD.Studio.PLAYBACK_STATE state;
            //openEmitter.EventInstance.getPlaybackState(out state);
            //if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            //{
            //    openEmitter.Play();
            //}

            if (!openEmitter.IsPlaying())
            {
                openEmitter.Play();
            }

            isOpen = true;
        }
    }

    IEnumerator CloseDoor()
    {
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }
        if (!needsOpen)
        {
            Debug.Log("Coroutine close");
            animator.Play("DoorClose");

            if (!closeEmitter.IsPlaying())
            {
                closeEmitter.Play();
            }

            isOpen = false;
        }
    }
}
