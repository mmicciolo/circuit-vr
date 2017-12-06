using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit23 : Puzzle
    {
        public LEDCircuitComponent[] LEDs;
        private bool alternate = false;

        private void Start()
        {
            InitPuzzle();
            puzzleName = "PuzzleCircuit23";
            endDuration = 5;
        }

        private void Update()
        {
            CheckCompletion();
            //Puzzle completion condition
            if ((choices[0].attachedComponent.componentPosition.x == outputPosition.componentPosition.x) && (choices[0].attachedComponent.componentPosition.y == outputPosition.componentPosition.y) && (stepsSinceCompletion == 0))
            {
                ActivateCells(0);
                ActivateCells(1);
                ActivateCells(4);
                ActivateCells(5);
                completed = true;
                DisableDragging();
            }
        }

        protected override void AnimateEnd()
        {
            if (alternate) ActiveFirstRow();
            else ActiveSecondRow();

            if ((stepsSinceCompletion % 40) == 0)
            alternate = !alternate;
        }

        private void ActiveFirstRow()
        {
            LEDs[0].lighted = true;
            LEDs[1].lighted = true;
            LEDs[2].lighted = false;
            LEDs[3].lighted = false;
        }

        private void ActiveSecondRow()
        {
            LEDs[0].lighted = false;
            LEDs[1].lighted = false;
            LEDs[2].lighted = true;
            LEDs[3].lighted = true;
        }

    }
}
