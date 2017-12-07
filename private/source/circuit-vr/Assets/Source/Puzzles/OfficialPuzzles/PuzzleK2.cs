﻿using System;
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
        

        public DraggableCircuitComponent[] choices;

        private void Start()
        {
            //ActivateCells(0);
        }

        private void Update()
        {
            if ((choices[2].attachedComponent.componentPosition.x == outputPosition.componentPosition.x) && (choices[2].attachedComponent.componentPosition.y == outputPosition.componentPosition.y))
            {
                Debug.Log("Puzzle solved");
                ActivateCells(0);
                ActivateCells(3);
                LevelController.getInstance().closePuzzle("PuzzleK2");
                LevelController.getInstance().puzzleController.SetComplete(1);
            }
        }
    }
}
