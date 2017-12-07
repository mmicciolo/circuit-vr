using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit17 : Puzzle
    {
        public SwitchCircuitComponent switch1;

        private bool close = false;

        private void Start()
        {
            InitPuzzle(4);
            puzzleName = "PuzzleCircuit17";
            endDuration = 5;
        }

        private void Update()
        {
            if ((choices[1].attachedComponent.componentPosition.x == outputPosition.componentPosition.x) && (choices[1].attachedComponent.componentPosition.y == outputPosition.componentPosition.y) && !close)
            {
				MarkCompleted ();
                DisableDragging();
                switch (switch1.lastAnimation)
                {
                    case "switch_up":
                        ActivateCells(0);
                        ActivateCells(1);
                        ActivateCells(3);
                        ActivateCells(4);
                        ActivateCells(6);
                        break;
                    case "switch_down":
                        ActivateCells(0);
                        ActivateCells(1);
                        ActivateCells(3);
                        ActivateCells(2);
                        ActivateCells(6);
                        break;
                    default:
                        DeactivateCells(0);
                        DeactivateCells(1);
                        DeactivateCells(2);
                        DeactivateCells(3);
                        DeactivateCells(4);
                        DeactivateCells(6);
                        break;
                }
            }
            CheckCompletion();
        }

        protected override void AnimateEnd()
        {
        }
    }
}
