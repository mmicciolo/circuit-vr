using Assets.Source.Puzzles.Grids;
using Assets.Source.Puzzles.Grids.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles.Components
{
    class ClickableCircuitComponent : CircuitComponent
    {
        public bool moved = false;
        public Vector2 initialPos;
        Puzzle currentPuzzle;

        private void Start()
        {
            initialPos = componentPosition;
            currentPuzzle = GameObject.FindObjectOfType<Puzzle>();
        }

        private void Update()
        {
        }

        private void OnMouseUpAsButton()
        {
            if (!moved)
            {
                currentPuzzle.ResetChoices();
                moved = true;

                Vector2 newPos = currentPuzzle.outputPosition.componentPosition;
                setComponentToCell(newPos);
            }
            else
            {
                moved = false;
                setComponentToCell(initialPos);
            }
        }
    }
}
