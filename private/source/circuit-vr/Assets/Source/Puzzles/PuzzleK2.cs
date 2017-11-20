using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;

namespace Assets.Source.Puzzles
{
    class PuzzleK2 : Puzzle
    {
        public DraggableCircuitComponent[] choices;

        private void Start()
        {
            //ActivateCells(0);
        }

        private void Update()
        {
            if ((choices[2].componentPosition.x == outputPosition.componentPosition.x) && (choices[2].componentPosition.y == outputPosition.componentPosition.y))
            {
                Debug.Log("Puzzle solved");
            }
        }

        override
        public void ResetChoices()
        {
            for (int i = 0; i < choices.Length; i++)
            {
                choices[i].moved = false;
                choices[i].setComponentToCell(choices[i].initialPos);
            }
        }
    }
}
