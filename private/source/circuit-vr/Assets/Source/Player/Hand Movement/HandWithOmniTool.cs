using Assets.Source.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWithOmniTool : MonoBehaviour {
    Animator animator;
    bool needsUp;
    bool isUp;
    FirstPersonPlayer fps;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        needsUp = false;
        isUp = false;
        fps = GetComponentInParent<FirstPersonPlayer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (fps.isNearInteractable() || DialogueManager.Instance.IsDialogPlaying()) needsUp = true;
        else needsUp = false;
        if (needsUp != isUp)
        {
            StartCoroutine(needsUp ? HandUp() : HandDown());
        }
    }

    IEnumerator HandUp()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        if (needsUp)
        {
            animator.Play("Up");
            isUp = true;
        }
    }

    IEnumerator HandDown()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        if (!needsUp)
        {
            animator.Play("Down");
            isUp = false;
        }
    }
}
