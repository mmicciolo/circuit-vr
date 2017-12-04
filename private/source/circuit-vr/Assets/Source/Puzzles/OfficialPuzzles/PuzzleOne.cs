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
            //ActivateCells(0);
        }

        private void Update()
        {
            if (outputPosition.activated)
            {
                Debug.Log("Puzzle solved");
                LevelController.getInstance().closePuzzle("PuzzleOne");

            }

            switch (switches[0].lastAnimation)
            {
                case "switch_up":
                    ActivateCells(2);
                    ActivateCells(1);
                    switch(switches[1].lastAnimation)
                    {
                        case "switch_up":
                            ActivateCells(4);
                            ActivateCells(5);
                            DeactivateCells(3);
                            break;
                        case "switch_down":
                            ActivateCells(4);
                            ActivateCells(3);
                            DeactivateCells(5);
                            break;
                        default:
                            DeactivateCells(4);
                            DeactivateCells(3);
                            DeactivateCells(5);
                            break;
                    }
                    break;
                case "switch_down":
                    ActivateCells(3);
                    ActivateCells(1);
                    switch (switches[1].lastAnimation)
                    {
                        case "switch_down":
                            ActivateCells(4);
                            ActivateCells(2);
                            DeactivateCells(5);
                            break;
                        default:
                            DeactivateCells(4);
                            DeactivateCells(2);
                            DeactivateCells(5);
                            break;
                    }
                    break;
                default:
                    DeactivateCells(1);
                    DeactivateCells(2);
                    DeactivateCells(3);
                    DeactivateCells(4);
                    DeactivateCells(5);
                    break;
            }
        }

    }
}
