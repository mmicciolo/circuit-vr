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

        private void OnTriggerEnter(Collider other)
        {
            if(checkBefore)
            {
                if(LevelController.getInstance().CheckDoorCanOpen(puzzleBefore))
                {

                    if (playOnce)
                    {
                        playOnce = false;
                        DialogueManager.Instance.StartDialogue(dialogName, pausePlayer);
                    }
                }
            }
            else
            {
                if (playOnce)
                {
                    playOnce = false;
                    DialogueManager.Instance.StartDialogue(dialogName, pausePlayer);
                }
            }
        }
    }
}
