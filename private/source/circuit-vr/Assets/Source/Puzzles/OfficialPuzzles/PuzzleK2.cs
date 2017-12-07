using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleK2 : Puzzle
    {
        
        private void Start()
        {
			InitPuzzle();
			puzzleName = "PuzzleK2";
			endDuration = 5;
        }

        private void Update()
        {
            if ((choices[2].attachedComponent.componentPosition.x == outputPosition.componentPosition.x) && (choices[2].attachedComponent.componentPosition.y == outputPosition.componentPosition.y))
            {
                ActivateCells(0);
				completed = true;
            }

			CheckCompletion ();
        }
    }
}
