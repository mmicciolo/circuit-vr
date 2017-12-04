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
        private bool fadingOut = true;
        private bool fadingIn = false;
        private bool firstFade = true;

        Material gridMaterial;
        Color startColor;
        Color endColor;
        float t = 0;
        float fadeTime = 2.5f;

        private void Start()
        {
            gridMaterial = Resources.Load("grid", typeof(Material)) as Material;
            startColor = gridMaterial.color;
            endColor = new Color(gridMaterial.color.r, gridMaterial.color.g, gridMaterial.color.b, 0);
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
                DrawGrid((int)gridSize.y, (int)gridSize.x, cellSize.y, cellSize.x, gridMaterial);
                foreach(CircuitComponent cc in GetComponentsInChildren<CircuitComponent>())
                {
                    if(!cc.toolboxItem)
                    {
                        cc.InitComponent();
                    }
                }
                AddGridCollider();
            }
            CheckForMouseOver();

            //Check for grid fading
            UpdateGridFadeIn();
            UpdateGridFadeOut();
        }

        private void DrawGrid(int gridHeight, int gridWidth, float cellHeight, float cellWidth, Material material)
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
                    cell.DrawCell(cellHeight, cellWidth, material);
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
                            fadingIn = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                if(!gridHidden)
                {
                    fadingOut = true;
                }
            }
        }

        private void AddGridCollider()
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>() as BoxCollider;
            collider.center = new Vector3((gridSize.x / 2) * cellSize.x, ((gridSize.y / 2) * cellSize.y) - cellSize.y, transform.localPosition.z);
            collider.size = new Vector3(gridSize.x * cellSize.x, gridSize.y * cellSize.y, 1.0f);
        }

        public void UpdateGridFadeOut()
        {
            if(fadingOut)
            {
                Color color = Color.Lerp(startColor, endColor, t);
                foreach (GameObject cell in cellObjects)
                {
                    foreach (LineRenderer line in cell.GetComponentsInChildren<LineRenderer>())
                    {
                        line.material.color = color;
                    }
                }
                if (t < 1)
                { 
                    t += Time.deltaTime / fadeTime;
                }
                else
                {
                    fadingOut = false;
                    gridHidden = true;
                    t = 0;
                    if(firstFade) { fadeTime = 1.0f; firstFade = false; }
                }
            }
        }

        public void UpdateGridFadeIn()
        {
            if (fadingIn)
            {
                Color color = Color.Lerp(endColor, startColor, t);
                foreach (GameObject cell in cellObjects)
                {
                    foreach (LineRenderer line in cell.GetComponentsInChildren<LineRenderer>())
                    {
                        line.material.color = color;
                    }
                }
                if (t < 1)
                { 
                    t += Time.deltaTime / fadeTime;
                }
                else
                {
                    fadingIn = false;
                    gridHidden = false;
                    t = 0;
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
