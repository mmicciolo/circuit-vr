using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit33 : Puzzle
    {
        public SwitchCircuitComponent[] switches;

        private void Start()
        {
			InitPuzzle();
			puzzleName = "PuzzleCircuit33";
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

            switch (switches[0].lastAnimation)
            {
                case "switch_up":
                    ActivateCells(4);
                    DeactivateCells(1);
                    break;
                case "switch_down":
                    ActivateCells(1);
                    DeactivateCells(4);
                    break;
                default:
                    DeactivateCells(4);
                    DeactivateCells(1);
                    break;
            }


            if (switches[1].lastAnimation == "switch_down")
            {
                ActivateCells(2);
            }
            else
            {
                DeactivateCells(2);
            }

            if ((switches[0].lastAnimation == "switch_up") && (switches[1].lastAnimation == "switch_down"))
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
