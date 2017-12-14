using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using FMODUnity;

public class DialogueManager : MonoBehaviour {

    //subtitles
    private string[] fileLines;
    //subs
    List<string> subLines = new List<string>();

    List<string> subTimingStr = new List<string>();
    public List<float> subTiming = new List<float>();

    public List<string> subText = new List<string>();

    int nextSub = 0;

    public string displaySub;
    //triggers
    List<string> triggerLines = new List<string>();

    List<string> triggerTimingStr = new List<string>();
    public List<float> triggerTiming = new List<float>();

    List<string> triggers = new List<string>();
    public List<string> triggerObjectNames = new List<string>();
    List<string> triggerMethodNames = new List<string>();

    //GUIStyle
    GUIStyle subStyle = new GUIStyle();

    int nextTrigger = 0;

    //dialogue and music play
    public static DialogueManager Instance { get; private set; }
    //bool isPlayingMusic = false;
    AudioClip MusicClip;
    AudioClip DialogueClip;
    private const float SAMPLE_RATE = 44100f;

    StudioEventEmitter soundEmitter;
    string dialogName;
    bool linePlaying = false;
    bool pressed = false;
    bool dialogPlaying = false;

    int lineCount = 1;

    void Awake()
    {
        if(Instance!= null && Instance!=this)
        {
            Destroy(gameObject);

        }
        Instance = this;

        gameObject.AddComponent<AudioSource>();

    }
    // only for 
    private string CleanSubTimings(string textToClean)
    {
        Regex digitsOnly = new Regex(@"^\d+(\.\d+)*$");
        return digitsOnly.Replace(textToClean, "");
    }

    public bool IsPlaying()
    {
        if(soundEmitter == null) {  return false;}
        FMOD.Studio.PLAYBACK_STATE state;
        soundEmitter.EventInstance.getPlaybackState(out state);
        if(state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDialogPlaying()
    {
        return dialogPlaying;
    }

    public void StartDialogue(string dialogName, bool pausePlayer)
    {
        if(pausePlayer)
        {
            LevelController.getInstance().Pause();
        }
        StartDialogue(dialogName);
    }

    public void StopDialog()
    {
        if (IsDialogPlaying())
        {
            lineCount = 1;
            soundEmitter.Stop();
            Destroy(soundEmitter);
            soundEmitter = null;
            dialogPlaying = false;
        }
    }

    public void StartNewDialog(string dialogName)
    {

        StopDialog();

        string[] split = dialogName.Split('/');
        this.dialogName = dialogName;

        //Load all of the lines of dialog
        TextAsset subfile = Resources.Load("Subtitles/" + split[split.Length - 1]) as TextAsset;
        fileLines = subfile.text.Split('\n');

        dialogPlaying = true;

        PlayNextLine();
    }

    public void PlayNextLine()
    {
        if(lineCount == fileLines.Length + 1)
        {
            lineCount = 1;
            soundEmitter.Stop();
            Destroy(soundEmitter);
            soundEmitter = null;
            dialogPlaying = false;
            return;
        }
        if(soundEmitter != null)
        {
            soundEmitter.Stop();
            Destroy(soundEmitter);
            soundEmitter = null;
        }
        soundEmitter = gameObject.AddComponent<StudioEventEmitter>();

        //Set the first line
        displaySub = fileLines[lineCount - 1];

        soundEmitter.Event = "event:/" + dialogName + "." + lineCount;

        //Load the first audio cue
        //if (!dialogName.Contains("Cue"))
        //{
        //    soundEmitter.Event = "event:/AI Dialogue/" + dialogName;
        //}
        //else
        //{
        //    soundEmitter.Event = "event:/Antagonist Dialogue/" + dialogName + "/" + dialogName + "." + lineCount;
        //}

        soundEmitter.Play();
        lineCount++;
    }

    public void StartDialogue (string dialogName)//AudioClip audioClip)
    {
        //MusicClip = music;
        //DialogueClip = audioClip;

        subLines = new List<string>();
        subTimingStr = new List<string>();
        subTiming = new List<float>();
        subText = new List<string>();

        /*
        triggerLines = new List<string>();
        triggerTimingStr = new List<string>();
        triggerTiming = new List<float>();
        triggers = new List<string>();
        triggerObjectNames = new List<string>();
        triggerMethodNames = new List<string>();
        */
        nextSub = 0;
        //nextTrigger = 0;



        //load text file
        //Debug.Log(DialogueClip.name);
        TextAsset subfile = Resources.Load("Subtitles/"+dialogName) as TextAsset;
        fileLines = subfile.text.Split('\n');

        
        //Debug.Log(fileLines[0]);
        //parsing time!
        foreach(string line in fileLines)
        {
            if (line.Contains("<trigger/>"))
            {
                triggerLines.Add(line); //if your line is a trigger
            }
            else
            {
                subLines.Add(line); //if your line is a subtitle text
            }
        }

        //separates subtitle text from timing
        for (int i=0;i<subLines.Count;i++)
        {
            string[] splitLine = subLines[i].Split('|');
            subTimingStr.Add(splitLine[0]);
            //Debug.Log(subTimingStr[0]);
            subTiming.Add(float.Parse(subTimingStr[i]));
            //Debug.Log(subTiming[0]);
            subText.Add(splitLine[1]);
        }
        //separates trigger text from timing
        /*for (int i=0;i<triggerLines.Count;i++)
        {
            string[] splitLine = triggerLines[i].Split('|');
            triggerTimingStr.Add(splitLine[0]);
            triggerTiming.Add(float.Parse(CleanSubTimings(triggerTimingStr[i])));
        }*/

        if (subText[0] != null)
        {
            displaySub = subText[0];
        }

        Debug.Log("AI started talking");

        soundEmitter = gameObject.AddComponent<StudioEventEmitter>();

        if (!dialogName.Contains("Cue"))
        {
            soundEmitter.Event = "event:/AI Dialogue/" + dialogName;
        }
        else
        {
            soundEmitter.Event = "event:/Antagonist Dialogue/" + dialogName;
        }

        soundEmitter.Play();

        //AudioSource audioSource = GetComponent<AudioSource>();
        //audioSource.clip = audioClip;
        //audioSource.loop = false;
        //audioSource.Play();
    }

    public void OnGUI()
    {
        if(soundEmitter != null)
        {
            if(Input.GetMouseButtonDown(0)) { pressed = true; }
            if (pressed && Input.GetMouseButtonUp(0))
            {
                pressed = false;
                PlayNextLine();
            }
            if (IsPlaying())
            {
                GUI.depth = -1000;
                subStyle.fixedWidth = Screen.width / 1.5f;
                subStyle.wordWrap = true;
                subStyle.alignment = TextAnchor.LowerCenter;
                subStyle.normal.textColor = Color.yellow;
                subStyle.fontSize = Mathf.FloorToInt(Screen.height * 0.025f);

                Vector2 size = subStyle.CalcSize(new GUIContent());
                GUI.contentColor = Color.black; //background for the subtitles
                GUI.Label(new Rect(Screen.width / 2 - size.x / 2 + 1, Screen.height / 1.25f - size.y + 1, size.x, size.y), displaySub, subStyle);
                GUI.contentColor = Color.white;
                GUI.Label(new Rect(Screen.width / 2 - size.x / 2, Screen.height / 1.25f - size.y, size.x, size.y), displaySub, subStyle);
            }
            if(soundEmitter != null)
            {
                int trackLength; soundEmitter.EventDescription.getLength(out trackLength);
                int trackPosition; soundEmitter.EventInstance.getTimelinePosition(out trackPosition);
                if (trackPosition > trackLength - 10)
                {
                    PlayNextLine();
                }
            }
        }
    }

    public void SetLineText()
    {

    }

    //public void OnGUI()
    //{
    //    //ensure that dialogue is on
    //    if(soundEmitter!=null)
    //    {
    //        //check for negative nextSub
    //        if (nextSub > 0 && !(subText[nextSub - 1].Contains("<break/>")))
    //        {
    //            GUI.depth = -1000;
    //            subStyle.fixedWidth = Screen.width / 1.5f;
    //            subStyle.wordWrap = true;
    //            subStyle.alignment = TextAnchor.LowerCenter;
    //            subStyle.normal.textColor = Color.yellow;
    //            subStyle.fontSize = Mathf.FloorToInt(Screen.height * 0.025f);

    //            Vector2 size = subStyle.CalcSize(new GUIContent());
    //            GUI.contentColor = Color.black; //background for the subtitles
    //            GUI.Label(new Rect(Screen.width / 2 - size.x / 2 + 1, Screen.height / 1.25f - size.y + 1, size.x, size.y), displaySub, subStyle);
    //            GUI.contentColor = Color.white;
    //            GUI.Label(new Rect(Screen.width / 2 - size.x / 2, Screen.height / 1.25f - size.y, size.x, size.y), displaySub, subStyle);
    //        }

    //        //next sub time
    //        if(nextSub<subText.Count)
    //        {
    //            int currentTime;
    //            soundEmitter.EventInstance.getTimelinePosition(out currentTime);
    //            float currentTimeSeconds = currentTime / 1000.0f;
    //            if (currentTimeSeconds > subTiming[nextSub])
    //            {
    //                displaySub = subText[nextSub];
    //                nextSub++;
    //            }
    //        }
    //        int trackLength; soundEmitter.EventDescription.getLength(out trackLength);
    //        int trackPosition; soundEmitter.EventInstance.getTimelinePosition(out trackPosition);
    //        if (trackPosition > trackLength - 100)
    //        {
    //            StartCoroutine(EndPause());
    //        }
    //    }
    //}

    public void Update()
    {

       
    }

    public IEnumerator EndPause()
    {
        yield return new WaitForSeconds(2.0f);
        LevelController.getInstance().isPaused = false;
        displaySub = "";
        Destroy(soundEmitter);
    }

    public IEnumerator DramaticPause(int pauseTime) //pause for 2 secs between AI talking and music start
    {
        yield return new WaitForSeconds(pauseTime);
    }
    
}
