using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit31 : Puzzle
    {
        public SwitchCircuitComponent[] switches;

        private void Start()
        {
			InitPuzzle();
			puzzleName = "PuzzleCircuit31";
			endDuration = 5;
        }

        private void Update()
        {
            if (outputPosition.activated)
            {
				completed = true;
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

            if ((switches[0].lastAnimation == "switch_down") || (switches[1].lastAnimation == "switch_down"))
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
