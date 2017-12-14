using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    //public AudioClip myClip;
    //public AudioClip music;

    // Use this for initialization
    void Start()
    {
        //AudioClip aistart = Resources.Load("Dialogue/intro") as AudioClip;
        //DialogueManager.Instance.StartDialogue(aistart);
        //DialogueManager.Instance.StartDialogue("Intro");
        //StartCoroutine(Fade());
        //DialogueManager.Instance.StartNewDialog("Antagonist Dialogue/Cue 1/Cue 1");
        //DialogueManager.Instance.StartNewDialog("AI Dialogue/Omni Explanation Intro/Omni Explanation Intro 1");

    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(30);
        DialogueManager.Instance.StartDialogue("Fail");
    }
}
