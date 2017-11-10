using Assets.Source.Puzzles.Grids;
using Assets.Source.Puzzles.Grids.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles.Components
{
    class CircuitComponent : MonoBehaviour
    {
        private bool dragging = false;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void Snap()
        {
            //Get the grid
            PuzzleGrid grid = GameObject.Find("Puzzle Grid").GetComponent<PuzzleGrid>();

            foreach(PuzzleCell cell in grid.gridCells)
            {

            }
        }
    }
}
