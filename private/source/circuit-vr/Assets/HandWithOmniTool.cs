using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWithOmniTool : MonoBehaviour {
    Animator animator;
    public bool isUp;

	// Use this for initialization
	void Start () {

         animator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		if (isUp)
        {
            animator.Play("Up");
        }
        else
        {
            animator.Play("Down");
        }
    }

    IEnumerator HandUp()
    {
            while (animator.GetBool("Up"))
            {
                yield return null;
            }
            animator.Play("Up");
            isUp = true;
    }

    public IEnumerator HandDown()
    {
        if (isUp)
        {
            while (animator.GetBool("Up"))
            {
                yield return null;
            }
            animator.Play("Down");
            isUp = false;
        }
    }
}
