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
        public DraggableCircuitComponent[] choices;

        private void Start()
        {
            //ActivateCells(0);
        }

        private void Update()
        {
            if ((choices[0].attachedComponent.componentPosition.x == outputPosition.componentPosition.x) && (choices[0].attachedComponent.componentPosition.y == outputPosition.componentPosition.y))
            {
                LevelController.getInstance().closePuzzle("PuzzleCircuit29");
            }
        }
    }
}
