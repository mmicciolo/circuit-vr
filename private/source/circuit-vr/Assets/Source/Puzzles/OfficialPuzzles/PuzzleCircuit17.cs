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
        public DraggableCircuitComponent[] choices;
        public SwitchCircuitComponent switch1;

        private bool close = false;

        private void Start()
        {
            //ActivateCells(0);
        }

        private void Update()
        {
            if ((choices[1].componentPosition.x == outputPosition.componentPosition.x) && (choices[1].componentPosition.y == outputPosition.componentPosition.y) && !close)
            {
                switch(switch1.lastAnimation)
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

                LevelController.getInstance().closePuzzle("PuzzleCircuit17");
            }
        }

        override
        public void ResetChoices()
        {
            Vector2 cell = new Vector2(0f, 0f);
            for (int i = 0; i < choices.Length; i++)
            {
                choices[i].moved = false;
                choices[i].setComponentToCell(cell);
                choices[i].transform.localPosition = choices[i].initialTransformPos;
            }
        }
    }
}
