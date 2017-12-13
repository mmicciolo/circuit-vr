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
        public bool toolboxItem = false;

        public bool activated = false;
        public bool activeMaterialActive = false;

        Material originalMaterial = null;

        private void Start()
        {
            GetOriginalMaterial();
        }

        protected void Update()
        {
            SetCorrectMaterial();
        }

        protected void SetCorrectMaterial()
        {
            if (activated && !activeMaterialActive)
            {
                GameObject cmodel = GetComponentModel();
                if (cmodel != null)
                {
                    cmodel.GetComponent<Renderer>().material = Resources.Load("glow_material", typeof(Material)) as Material;
                }
                activeMaterialActive = true;
            }
            else if (!activated && activeMaterialActive)
            {
                GameObject cmodel = GetComponentModel();
                if (cmodel != null)
                {
                    cmodel.GetComponent<Renderer>().material = originalMaterial;
                }
                activeMaterialActive = false;
            }
        }

        public void GetOriginalMaterial()
        {
            GameObject componentModel = GetComponentModel();
            if (componentModel != null)
            {
                originalMaterial = componentModel.GetComponent<Renderer>().material;
            }
        }

        private GameObject GetComponentModel()
        {
            foreach(Transform child in transform)
            {
                if(child.gameObject.name.Equals("bb"))
                {
                    return child.gameObject;
                }
            }
            return null;
        }

        //protected void GetOriginalMaterial()
        //{
        //    GameObject componentModel = GetComponentModel();
        //    if (componentModel != null)
        //    {
        //        originalMaterial = componentModel.GetComponent<Renderer>().material;
        //    }
        //}

        //private GameObject GetComponentModel()
        //{
        //    foreach(Transform child in transform)
        //    {
        //        if(child.gameObject.name.Equals("straight_wire_model") || child.gameObject.name.Equals("right_wire_model") || child.gameObject.name.Equals("3_way_tee_wire"))
        //        {
        //            return child.gameObject;
        //        }
        //        if(child.gameObject.name.Equals("switch_pole"))
        //        {
        //            foreach (Transform switchChild in child.gameObject.transform)
        //            {
        //                if(switchChild.gameObject.name.Equals("switch_wire"))
        //                {
        //                    return switchChild.gameObject;
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}

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
            setComponentToCell(componentPosition);

            Puzzle currentPuzzle = GameObject.FindObjectOfType<Puzzle>();
            if (currentPuzzle.components[group] == null)
            {
                currentPuzzle.components[group] = new List<GameObject>();
            }
            currentPuzzle.components[group].Add(gameObject);
        }

        public void setComponentToCell(Vector2 cellPos)
        {
            PuzzleGrid grid = PuzzleGrid.GetPuzzleGrid();
            
            PuzzleCell cell = grid.getCell(cellPos);
            if (cell == null)
            {
                Debug.Log(cellPos.x + " " + cellPos.y);
            }
            Vector3 pos = cell.transform.position;
            pos.x += (transform.localScale.x / 2); pos.y -= (transform.localScale.x / 2);
            transform.position = pos;
            transform.eulerAngles = new Vector3(0, 0, componentRotation);

            componentPosition = cellPos;
        }

        public bool isMoveable
        {
            get { return moveable; }
            set { moveable = value; }
        }

        public int getLength()
        {
            foreach(Transform child in transform)
            {
                if(child.name.Contains("wire") || child.name.Contains("switch"))
                {
                    return 1;
                }
                else
                {
                    return 3;
                }
            }
            return 0;
        }

        public Vector2 GetInfoPosition()
        {
            Vector2 result = gameObject.transform.position;
            /* if ((componentRotation % 180) == 0)
             {
                 result = new Vector2(result.x )
             } */
            Vector2 boxColliderSize = gameObject.GetComponent<BoxCollider>().size;
            Vector2 transformScale = gameObject.transform.localScale;

            switch(componentRotation)
            {
                case 90:
                    result = new Vector2( result.x , result.y + (float)((boxColliderSize.x - 0.5) * transformScale.x) + 0.7f);
                    break;
                default:
                    result = new Vector2(result.x, result.y + (float)(0.5 * transformScale.x) + 0.7f);
                    break;
            }
            return result;
        }
    }
}
