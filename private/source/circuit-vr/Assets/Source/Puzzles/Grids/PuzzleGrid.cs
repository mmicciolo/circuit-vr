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

        private bool gridHidden = false;

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
                    if(!cc.toolboxItem)
                    {
                        cc.InitComponent();
                    }
                }
                AddGridCollider();
                //HideGrid();
                StartCoroutine(ShowGridInitially());
            }
            CheckForMouseOver();
        }

        private System.Collections.IEnumerator ShowGridInitially()
        {
            while(true)
            {
                yield return new WaitForSeconds(2);
                HideGrid();
                gridHidden = true;
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

        //We cant use OnMouseEnter or OnMouseExit because of other colliders in the scene
        //Therefore we must use a raycast all and see if one of the objects is this puzzle grid
        public void CheckForMouseOver()
        {
            RaycastHit[] hits;
            Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray);
            if (hits.Length > 0)
            {
                foreach(RaycastHit hit in hits)
                {
                    if(hit.transform.name.Equals(transform.name))
                    {
                        if(gridHidden)
                        {
                            ShowGrid();
                            gridHidden = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                if(gridHidden)
                {
                    HideGrid();
                }
            }
        }

        private void AddGridCollider()
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>() as BoxCollider;
            collider.center = new Vector3((gridSize.x / 2) * cellSize.x, ((gridSize.y / 2) * cellSize.y) - cellSize.y, transform.localPosition.z);
            collider.size = new Vector3(gridSize.x * cellSize.x, gridSize.y * cellSize.y, 1.0f);
        }

        public void ShowGrid()
        {
            foreach (GameObject cell in cellObjects)
            {
                foreach (LineRenderer line in cell.GetComponentsInChildren<LineRenderer>())
                {
                    line.enabled = true;
                }
            }
        }

        public void HideGrid()
        {
            foreach(GameObject cell in cellObjects)
            {
                foreach(LineRenderer line in cell.GetComponentsInChildren<LineRenderer>())
                {
                    line.enabled = false;
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
