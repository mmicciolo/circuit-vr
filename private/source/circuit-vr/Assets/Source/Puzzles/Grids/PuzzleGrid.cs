using Assets.Source.Puzzles.Components;
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

        public Vector2 gridSize;
        public Vector2 cellSize;

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
                DrawGrid((int)gridSize.y, (int)gridSize.x, cellSize.y, cellSize.x);
                foreach(CircuitComponent cc in GetComponentsInChildren<CircuitComponent>())
                {
                    cc.InitComponent();
                }
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

        public PuzzleCell getCell(Vector2 position)
        {
            foreach(PuzzleCell cell in puzzleCells)
            {
                if(position.Equals(cell.GridPosition))
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

        public static PuzzleGrid GetPuzzleGrid()
        {
            return GameObject.Find("Puzzle Grid").GetComponent<PuzzleGrid>();
        }
    }
}
