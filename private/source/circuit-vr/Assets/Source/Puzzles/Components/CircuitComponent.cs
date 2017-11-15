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

        public Vector2 componentPosition;
        public int componentRotation;
        public int group;
        public bool moveable = true;

        public bool activated = false;

        private void Start()
        {

        }

        private void Update()
        {
        }

        public void Snap()
        {
            //Get the grid
            PuzzleGrid grid = PuzzleGrid.GetPuzzleGrid();

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

        public void InitComponent()
        {
            gameObject.transform.localScale = new Vector3(PuzzleGrid.GetPuzzleGrid().cellSize.x, PuzzleGrid.GetPuzzleGrid().cellSize.y, PuzzleGrid.GetPuzzleGrid().cellSize.x);
            setComponentToCell();

            Puzzle currentPuzzle = GameObject.FindObjectOfType<Puzzle>();
            if (currentPuzzle.components[group] == null)
            {
                currentPuzzle.components[group] = new List<GameObject>();
            }
            currentPuzzle.components[group].Add(gameObject);
        }

        public void setComponentToCell()
        {
            PuzzleGrid grid = PuzzleGrid.GetPuzzleGrid();
            PuzzleCell cell = grid.getCell(componentPosition);
            Vector3 pos = cell.transform.position;
            pos.x += (transform.localScale.x / 2); pos.y -= (transform.localScale.x / 2);
            transform.position = pos;
            transform.eulerAngles = new Vector3(0, 0, componentRotation);
        }

        public bool isMoveable
        {
            get { return moveable; }
            set { moveable = value; }
        }
    }
}
