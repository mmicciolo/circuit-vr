using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit26 : Puzzle
    {
        public SwitchCircuitComponent circuitSwitch;
        public CircuitComponent capacitor;

        int step;

        private void Start()
        {
            step = 0;
			stopwatch = new Stopwatch();
			stopwatch.Start();
			InitPuzzle(6);
			puzzleName = "PuzzleCircuit26";
			endDuration = 5;
			circuitSwitch.lastAnimation = "switch_down";
        }

        private void Update()
        {
			switch (circuitSwitch.lastAnimation) {
			case "switch_down":
				ActivateCells (0);
				ActivateCells (1);
				ActivateCells (2);
				ActivateCells (3);
				break;
			case "switch_up":
				ActivateCells (0);
				ActivateCells (1);
				ActivateCells (4);
				ActivateCells (3);
				break;
			default:
				DeactivateCells (0);
				DeactivateCells (1);
				DeactivateCells (2);
				DeactivateCells (3);
				DeactivateCells (4);
				break;
			}

			double time = 10 - stopwatch.Elapsed.TotalSeconds;
			//if (stopwatch.ElapsedTicks <= 1)
            //{
                //circuitSwitch.animator.Play("switch_down");
            //}

            if (circuitSwitch.lastAnimation != "switch_down")
            {
                UnityEngine.Debug.Log("Puzzle solved");
				MarkCompleted ();
			}
            else
            {
				capacitor.gameObject.GetComponent<DisplayInfo> ().notation = "Capacitor overload in " + (int)time;

				if (time <= 0) {
					UnityEngine.Debug.Log ("Capacitor broken");
					capacitor.gameObject.GetComponent<DisplayInfo> ().notation = "Capacitor broken";
					LevelController.getInstance ().closePuzzle ("PuzzleCircuit26");
				}
            }

			CheckCompletion ();

        }

    }
}
