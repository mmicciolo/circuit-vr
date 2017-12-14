using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCell : MonoBehaviour {

    public Animator animator;
    public bool activated;
    public int puzzleNumber;
    float cellGlowPower = 2.5f;
    float sphereGlowPower = 0.97f;
    Material cellGlowMaterial = null;
    Material sphereGlowMaterial = null;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.name.Equals("power_cell_low"))
            {
                cellGlowMaterial = t.gameObject.GetComponent<Renderer>().material;
            }
            if (t.name.Equals("power_sphere_hi"))
            {
                sphereGlowMaterial = t.gameObject.GetComponent<Renderer>().material;
            }
        }
        activated = false;
    }
	
	// Update is called once per frame
	void Update () {
        //activated = LevelController.getInstance().CheckDoorCanOpen(puzzleNumber);
        activated = true;

		if(activated)
        {
            cellGlowMaterial.SetFloat("_MKGlowPower", cellGlowPower);
            sphereGlowMaterial.SetFloat("_MKGlowPower", sphereGlowPower);
            animator.Play("powercell_outer_ring");
        }
        else
        {
            cellGlowMaterial.SetFloat("_MKGlowPower", 0);
            sphereGlowMaterial.SetFloat("_MKGlowPower", 0);
            animator.StopPlayback();
        }
	}
}
