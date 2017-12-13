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
		public LEDCircuitComponent[] LEDs;
		bool alternate = true;
		long lastAlternate;

        private void Start()
        {
			InitPuzzle(6);
			puzzleName = "PuzzleCircuit29";
			endDuration = 20;
        }

        private void Update()
        {
            if ((choices[0].attachedComponent.componentPosition.x == outputPosition.componentPosition.x) && (choices[0].attachedComponent.componentPosition.y == outputPosition.componentPosition.y))
            {
				ActivateCells(0);
				MarkCompleted ();
				DisableDragging();
            }
			CheckCompletion ();
        }

		protected override void AnimateEnd ()
		{
			long time = stopwatch.ElapsedMilliseconds - lastAlternate;
			if (time > 3000) {
				alternate = !alternate;
				lastAlternate = stopwatch.ElapsedMilliseconds;
			} else {
				if (alternate) {
					if (time > 200) {
						LEDs [0].lighted = true;
					}
					if (time > 800) {
						LEDs [1].lighted = true;
						LEDs [2].lighted = true;
					}
					if (time > 1400) {
						LEDs [3].lighted = true;
						LEDs [4].lighted = true;
						LEDs [5].lighted = true;
					}
					if (time > 2000) {
						LEDs [6].lighted = true;
						LEDs [7].lighted = true;
						LEDs [8].lighted = true;
						LEDs [9].lighted = true;
					}
				} else {
					foreach (LEDCircuitComponent led in LEDs) {
						led.lighted = false;
					}
				}
			}
				
		}
    }
}
