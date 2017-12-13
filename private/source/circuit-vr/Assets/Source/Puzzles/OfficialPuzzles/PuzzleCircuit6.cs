using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit6 : Puzzle
    {
        public LEDCircuitComponent LED;

        private void Start()
        {
            InitPuzzle(2);
            puzzleName = "PuzzleCircuit6";
            endDuration = 5;
        }

        private void Update()
        {
            if ((choices[1].attachedComponent.componentPosition.x == outputPosition.componentPosition.x) && (choices[1].attachedComponent.componentPosition.y == outputPosition.componentPosition.y))
            {
                ActivateCells(0);
				MarkCompleted ();
				LED.lighted = true;
                DisableDragging();
            }

            CheckCompletion();
        }
    }
}
