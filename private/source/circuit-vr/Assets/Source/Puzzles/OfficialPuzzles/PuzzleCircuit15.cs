using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit15 : Puzzle
    {
        public SPSTSwitchCircuitComponent circuitSwitch;
        public LEDCircuitComponent LED;

        private void Start()
        {
            InitPuzzle(5);
            puzzleName = "PuzzleCircuit15";
            endDuration = 5;
        }

        private void Update()
        {
            if ((choices[2].attachedComponent.componentPosition.x == outputPosition.componentPosition.x) && (choices[2].attachedComponent.componentPosition.y == outputPosition.componentPosition.y) && (circuitSwitch.lastAnimation == "spst_down"))
            {
                ActivateCells(0);
                ActivateCells(3);
				MarkCompleted ();
                LED.lighted = true;
                DisableDragging();
            }

			switch (circuitSwitch.lastAnimation) {
			case "spst_down":
				ActivateCells (0);
				ActivateCells (3);
				break;
			default:
				DeactivateCells (0);
				DeactivateCells (3);
				break;
			}
            CheckCompletion();
        }
    }
}
