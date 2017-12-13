using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit34 : Puzzle
    {
        public SwitchCircuitComponent[] switches;

        private void Start()
        {
			InitPuzzle(10);
			puzzleName = "PuzzleCircuit34";
			endDuration = 5;
        }

        private void Update()
        {
            if (outputPosition.activated)
            {
				MarkCompleted ();
				foreach (LEDCircuitComponent led in GetComponentsInChildren<LEDCircuitComponent>()) {
					led.lighted = true;
				}
            }

            if (switches[0].lastAnimation == "switch_down")
            {
                ActivateCells(1);
            }
            else
            {
                DeactivateCells(1);
            }

            if (switches[1].lastAnimation == "switch_down")
            {
                ActivateCells(2);
            }
            else
            {
                DeactivateCells(2);
            }

            if ((switches[0].lastAnimation == "switch_up") && (switches[1].lastAnimation == "switch_up"))
            {
                ActivateCells(3);
            }
            else
            {
                DeactivateCells(3);
            }

			CheckCompletion ();
        }

    }
}
