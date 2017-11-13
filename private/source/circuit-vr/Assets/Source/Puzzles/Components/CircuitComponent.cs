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
        private int sizeX = 0;
        private int sizeY = 0;

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

            //Loop through all of the cells in the grid
            float distance = 1000f;
            PuzzleCell closestCell = null;
            foreach(PuzzleCell cell in grid.gridCells)
            {
                //Get the distance to the cell
                float cellDistance = Vector2.Distance(new Vector2(transform.position.x - (transform.localScale.x / 2), transform.position.y + (transform.localScale.x / 2)), new Vector2(cell.transform.position.x, cell.transform.position.y));

                //If the distance is closer, set it as the new cloest cell and distance
                if (cellDistance < distance)
                {
                    closestCell = cell;
                    distance = cellDistance;
                }
            }

            //If we find a cell and the distance is within 5 units, snap to it
            if(closestCell != null && distance <= 5f)
            {
                //Snap to the cell
                Vector3 pos = closestCell.transform.position;
                pos.x += (transform.localScale.x / 2); pos.y -= (transform.localScale.x / 2);
                transform.position = pos;
            }
        }

        private void setObjectToCells(PuzzleCell cell)
        {

        }
    }
}
