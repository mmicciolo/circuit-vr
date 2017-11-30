using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

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
    bool isPlayingMusic = false;
    AudioClip MusicClip;
    AudioClip DialogueClip;
    private const float SAMPLE_RATE = 44100f;
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

    public void StartDialogue (AudioClip audioClip,AudioClip music)
    {
        MusicClip = music;
        DialogueClip = audioClip;

        subLines = new List<string>();
        subTimingStr = new List<string>();
        subTiming = new List<float>();
        subText = new List<string>();

        triggerLines = new List<string>();
        triggerTimingStr = new List<string>();
        triggerTiming = new List<float>();
        triggers = new List<string>();
        triggerObjectNames = new List<string>();
        triggerMethodNames = new List<string>();

        nextSub = 0;
        nextTrigger = 0;

        

        //load text file
        TextAsset subfile = Resources.Load("Subtitles/subs") as TextAsset;
        fileLines = subfile.text.Split('\n');

        
        //Debug.Log(fileLines[0]);
        //parsingtime!
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


        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void OnGUI()
    {
        //ensure that dialogue is on
        if(DialogueClip!=null && !isPlayingMusic)
        {
            //check for negative nextSub
            if (nextSub > 0 && !(subText[nextSub - 1].Contains("<break/>")))
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

            //next sub time
            if(nextSub<subText.Count)
            {
                if(GetComponent<AudioSource>().timeSamples/SAMPLE_RATE > subTiming[nextSub])
                {
                    displaySub = subText[nextSub];
                    nextSub++;
                }
            }
            
        }
    }

    public void Update()
    {

        if (!GetComponent<AudioSource>().isPlaying && !isPlayingMusic)
        {
            Debug.Log("AI stopped talking");
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = MusicClip;
            DramaticPause(2);
            GetComponent<AudioSource>().Play();
        }
    }

    public IEnumerator DramaticPause(int pauseTime) //pause for 2 secs between AI talking and music start
    {
        yield return new WaitForSeconds(pauseTime);
    }
    
}
