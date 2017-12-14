using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source
{
    class DialogTrigger : MonoBehaviour
    {
        public string dialogName;
        public bool pausePlayer = false;
        public bool playOnce = true;
        public bool checkBefore = false;
        public int puzzleBefore;
        public bool handUp = false;

        private void OnTriggerEnter(Collider other)
        {
            if (dialogName.Contains("Omni Explanation 1"))
            {
                GameObject.Find("HandWithOmniTool").GetComponent<HandWithOmniTool>().forceUp = true;
                handUp = true;
            }
            if (checkBefore)
            {
                if(LevelController.getInstance().CheckDoorCanOpen(puzzleBefore))
                {

                    if (playOnce)
                    {
                        playOnce = false;
                        DialogueManager.Instance.StartNewDialog(dialogName);
                    }
                }
            }
            else
            {
                if (playOnce)
                {
                    playOnce = false;
                    DialogueManager.Instance.StartNewDialog(dialogName);
                }
            }
        }

        private void Update()
        {
            if(handUp == true)
            {
                if(!DialogueManager.Instance.IsDialogPlaying())
                {
                    handUp = true;
                    GameObject.Find("HandWithOmniTool").GetComponent<HandWithOmniTool>().forceUp = false;
                }
            }
        }
    }
}
