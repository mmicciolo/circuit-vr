using Assets.Source.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandWithOmniTool : MonoBehaviour
{
    Animator animator;
    public bool needsUp;
    public bool forceUp = false;
    bool isUp;
    bool isAnimationDone;
    FirstPersonPlayer fps;

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        needsUp = false;
        isUp = false;
        fps = GetComponentInParent<FirstPersonPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fps.isNearInteractable() /*|| DialogueManager.Instance.IsDialogPlaying()*/ || forceUp) needsUp = true;
        else needsUp = false;
        if (needsUp != isUp)
        {
            StartCoroutine(needsUp ? HandUp() : HandDown());
        }
    }

    IEnumerator HandUp()
    {
        while ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) && (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_down")))
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
        while ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) && (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
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
