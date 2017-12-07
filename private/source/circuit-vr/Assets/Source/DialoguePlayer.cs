using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour {
    //public AudioClip myClip;
    //public AudioClip music;

	// Use this for initialization
	void Start () {
        AudioClip aistart = Resources.Load("Dialogue/intro") as AudioClip;
        DialogueManager.Instance.StartDialogue(aistart);
        
	}
   
}
