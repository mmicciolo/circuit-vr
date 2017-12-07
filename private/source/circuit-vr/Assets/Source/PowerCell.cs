using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCell : MonoBehaviour {

    public Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.Play("powercell_outer_ring");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
