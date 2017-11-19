using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;

namespace Assets.Source.Puzzles
{
    class PuzzleTwo : Puzzle
    {
        public SwitchCircuitComponent[] switches;

        private void Start()
        {
            //ActivateCells(0);
        }

        private void Update()
        {
            if (outputPosition.activated)
            {
                Debug.Log("Puzzle solved");
            }


            switch (switches[0].lastAnimation)
            {
                case "switch_up":
                    ActivateCells(2);
                    DeactivateCells(3);
                    DeactivateCells(4);
                    DeactivateCells(6);
                    DeactivateCells(7);
                    break;
                case "switch_down":
                    ActivateCells(3);
                    ActivateCells(4);
                    ActivateCells(6);
                    ActivateCells(7);
                    DeactivateCells(2);
                    break;
                default:
                    DeactivateCells(2);
                    DeactivateCells(3);
                    DeactivateCells(4);
                    DeactivateCells(6);
                    DeactivateCells(7);
                    break;
            }

        }
    }
}
