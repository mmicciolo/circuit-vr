using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Puzzles
{
    class DisplayInfo : MonoBehaviour
    {
        Text myText;
        bool displaying;
        public string notation;
        float fadeTime;

        private void Start()
        {
            myText = GameObject.FindObjectOfType<Puzzle>().infoText;
            myText.color = Color.clear;
            fadeTime = 0.5f;
        }

        private void Update()
        {
            if (displaying)
            {
                myText.text = notation;
                myText.color = Color.white; //Color.Lerp(myText.color, Color.white, fadeTime * Time.deltaTime);
            }
            else
            {
                myText.color = Color.Lerp(myText.color, Color.clear, fadeTime * Time.deltaTime);
            }
        }

        private void OnMouseOver()
        {
            //myText.transform.position = gameObject.transform.position;
            //Debug.Log("detecting");
            //Debug.Log(myText.transform.position.x + ", " + myText.transform.position.y + ", " + myText.transform.position.z);
            displaying = true;
        }

        private void OnMouseExit()
        {
            displaying = false;
        }


    }
}
