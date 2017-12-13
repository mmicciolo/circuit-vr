using Assets.Source.Puzzles.Components;
using Assets.Source.Puzzles.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles.Toolboxes
{
    class ToolBox : MonoBehaviour
    {

        public void Start()
        {
            //Set the toolbox position the same as the grid
            transform.position = PuzzleGrid.GetPuzzleGrid().transform.position;

            //Set the toolbox Y pos
            float toolBoxItemYPos = (PuzzleGrid.GetPuzzleGrid().gridSize.y * PuzzleGrid.GetPuzzleGrid().cellSize.y) + 2.0f;

            //Get the circuit components
            DraggableCircuitComponent[] components = GetComponentsInChildren<DraggableCircuitComponent>();

            for(int i = 0; i < components.Length; i++)
            {
                //Set the scale
                components[i].transform.localScale = new Vector3(PuzzleGrid.GetPuzzleGrid().cellSize.x, PuzzleGrid.GetPuzzleGrid().cellSize.y, PuzzleGrid.GetPuzzleGrid().cellSize.x);

                if(i == 0)
                {
                    //Set the position
                    components[i].transform.localPosition = new Vector3((PuzzleGrid.GetPuzzleGrid().cellSize.x / 2), toolBoxItemYPos - (PuzzleGrid.GetPuzzleGrid().cellSize.y / 2), 0);
                }
                else
                {
                    //Set the position
                    components[i].transform.localPosition = new Vector3((i * (2.0f + (components[i - 1].getLength() * PuzzleGrid.GetPuzzleGrid().cellSize.x))) + (PuzzleGrid.GetPuzzleGrid().cellSize.x / 2), toolBoxItemYPos - (PuzzleGrid.GetPuzzleGrid().cellSize.y / 2), 0);
                }

                //Set the components initial position
                components[i].initialTransformPos = components[i].transform.localPosition;
            }
        }
    }
}
