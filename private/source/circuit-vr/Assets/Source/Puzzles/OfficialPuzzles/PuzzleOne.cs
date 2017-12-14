using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleOne : Puzzle
    {
        public SwitchCircuitComponent[] switches;

        private void Start()
        {
			InitPuzzle (0);
            puzzleName = "PuzzleOne";
        }

        private void Update()
        {
            if (outputPosition.activated)
            {
                Debug.Log("Puzzle solved");
                MarkCompleted();
                endDuration = 3;
            }

            if ((switches[0].lastAnimation == "switch_up") && (switches[1].lastAnimation == "switch_up"))
            {
                ActivateCells(0);
                ActivateCells(2);
                ActivateCells(1);
                ActivateCells(4);
                ActivateCells(5);
            } else
            {
                DeactivateCells(0);
                DeactivateCells(1);
                DeactivateCells(2);
                DeactivateCells(3);
                DeactivateCells(4);
                DeactivateCells(5);
            }

            CheckCompletion();
        }

        protected override void ClosePuzzle()
        {
            LevelController.getInstance().lockBehind[3].puzzleNumber = 1;
            LevelController.getInstance().lockBehind[2].puzzleNumber = 10;
            base.ClosePuzzle();
        }
    }
}
