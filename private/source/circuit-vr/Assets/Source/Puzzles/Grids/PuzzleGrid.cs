using Assets.Source.Puzzles.Grids.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles.Grids
{
    class PuzzleGrid : MonoBehaviour
    {
        private List<PuzzleCell> puzzleCells = new List<PuzzleCell>();
        private List<GameObject> cellObjects = new List<GameObject>();

        private void Start()
        {
            
        }

        private void Update()
        {
            if (gameObject.transform.hasChanged)
            {
                foreach(GameObject obj in cellObjects) {
                    Destroy(obj);
                }
                puzzleCells = new List<PuzzleCell>();
                cellObjects = new List<GameObject>();
                gameObject.transform.hasChanged = false;
                DrawGrid(10, 10, 2.5f, 2.5f);
            }
        }

        private void DrawGrid(int gridHeight, int gridWidth, float cellHeight, float cellWidth)
        {
            for(int height = 0; height < gridHeight; height++)
            {
                for(int width = 0; width < gridWidth; width++)
                {
                    GameObject cellGameObject = new GameObject("Cell " + height + " " + width);
                    cellGameObject.AddComponent<PuzzleCell>();
                    PuzzleCell cell = cellGameObject.GetComponent<PuzzleCell>();
                    cell.GridPosition = new Vector2(width, height);
                    cellGameObject.transform.position = gameObject.transform.position + new Vector3(width * cellWidth, height * cellHeight, 0);
                    cell.DrawCell(cellHeight, cellWidth);
                    cellGameObject.transform.parent = gameObject.transform;
                    cellObjects.Add(cellGameObject);
                    puzzleCells.Add(cell);
                }
            }
        }

        public PuzzleCell getCell(int x, int y)
        {
            foreach(PuzzleCell cell in puzzleCells)
            {
                if(new Vector2(y, x).Equals(cell.GridPosition))
                {
                    return cell;
                }
            }
            return null;
        }

        public List<PuzzleCell> gridCells
        {
            get { return puzzleCells; }
        }
    }
}
