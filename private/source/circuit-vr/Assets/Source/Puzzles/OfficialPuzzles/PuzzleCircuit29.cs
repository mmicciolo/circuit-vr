using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit29 : Puzzle
    {

        private void Start()
        {
			InitPuzzle();
			puzzleName = "PuzzleCircuit29";
			endDuration = 5;
        }

        private void Update()
        {
            if ((choices[0].attachedComponent.componentPosition.x == outputPosition.componentPosition.x) && (choices[0].attachedComponent.componentPosition.y == outputPosition.componentPosition.y))
            {
				ActivateCells(0);
				completed = true;
				foreach (LEDCircuitComponent led in GetComponentsInChildren<LEDCircuitComponent>()) {
					led.lighted = true;
				}
				DisableDragging();
            }
			CheckCompletion ();
        }
    }
}
