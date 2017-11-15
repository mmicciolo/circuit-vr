using Assets.Source.Puzzles.Grids;
using Assets.Source.Puzzles.Grids.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles.Components
{
    class ClickableCircuitComponent : MonoBehaviour
    {

        private void Start()
        {

        }

        private void Update()
        {

        }

        private void OnMouseDown()
        {
            //Get the current screen point
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);

            //Based off of the above point calculate a transform
            Vector3 curPosition = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(curScreenPoint);

            //Set the transform to the dragging object
            //Lock the Z position
            transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);
        }

        private void OnMouseUp()
        {
            //See if the object is on the grid
            Snap();
        }

        public void Snap()
        {
            //Get the grid
            PuzzleGrid grid = GameObject.Find("Puzzle Grid").GetComponent<PuzzleGrid>();

            float distance = 1000f;
            PuzzleCell closestCell = null;
            foreach (PuzzleCell cell in grid.gridCells)
            {
                float cellDistance = Vector2.Distance(new Vector2(transform.position.x - 1.25f, transform.position.y + 1.25f), new Vector2(cell.transform.position.x, cell.transform.position.y));
                if (cellDistance < distance)
                {
                    closestCell = cell;
                    distance = cellDistance;
                }
            }

            if (closestCell != null && distance <= 5f)
            {
                Vector3 pos = closestCell.transform.position;
                pos.x += 1.25f; pos.y -= 1.25f;
                transform.position = pos;
            }
        }
    }
}
