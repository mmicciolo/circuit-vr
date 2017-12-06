﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Source.Puzzles.Components;
using Assets.Source.Puzzles.Grids;

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
                Vector2 atCell = gameObject.transform.position;
                Vector2 scale = GameObject.Find("Puzzle Grid").GetComponent<PuzzleGrid>().cellSize;
                Vector2 textPosition = new Vector2(atCell.x - scale.x, atCell.y + scale.y);
                myText.transform.position = gameObject.GetComponent<CircuitComponent>().GetInfoPosition();
                myText.text = notation;
                myText.color = Color.white; 
            }
            else
            {
                myText.color = Color.Lerp(myText.color, Color.clear, fadeTime * Time.deltaTime);
            }
        }

        private void OnMouseEnter()
        {
 
            //myText.color = Color.Lerp(Color.black, Color.yellow, fadeTime * Time.deltaTime);
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
