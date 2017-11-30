using Assets.Source.Interactable_Objects;
using Assets.Source.Puzzles.Components;
using Assets.Source.Puzzles.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Puzzles
{
    class Puzzle : InteractablePuzzle
    {
        private PuzzleGrid puzzleGrid;

        private bool dragging = false;
        GameObject draggingObject = null;
        new Camera camera = null;

        public CircuitComponent outputPosition;

        public List<GameObject>[] components = new List<GameObject>[10];
        public Text infoText;

        private void Start()
        {
            puzzleGrid = PuzzleGrid.GetPuzzleGrid();
            camera = GameObject.Find("Main Camera").GetComponent<Camera>();

            infoText.transform.position = new Vector3(13f, -5f, 9f);
        }

        public Camera GetCamera()
        {
            return camera;
        }

        private void Update()
        {
            ////Wait for the user to press down the left mouse button
            //if (Input.GetMouseButtonDown(0))
            //{
            //    //Perform a raycast using the mouse position
            //    RaycastHit hit;
            //    Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            //    //If we hit something
            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        CircuitComponent childComponent = hit.transform.gameObject.GetComponent<CircuitComponent>();
            //        CircuitComponent parentComponent = hit.transform.parent.gameObject.GetComponent<CircuitComponent>();
            //        //Check to see if the transform object has the component
            //        if (childComponent != null && childComponent.isMoveable)
            //        {
            //            //Hit the parent
            //            Debug.Log("Hit Parent");

            //            //Start dragging
            //            dragging = true;

            //            draggingObject = hit.transform.gameObject;
            //        }
            //        else if(parentComponent != null && parentComponent.isMoveable)
            //        {
            //            //Hit the child
            //            Debug.Log("Hit Child");

            //            //Start dragging
            //            dragging = true;

            //            draggingObject = hit.transform.parent.gameObject;
            //        }
            //    }
            //}

            ////If they let go of the mouse button
            //if (Input.GetMouseButtonUp(0))
            //{
            //    if(dragging == true)
            //    {
            //        //See if the object is on the grid
            //        draggingObject.GetComponent<CircuitComponent>().Snap();

            //        //They are no longer dragging
            //        dragging = false;
            //        draggingObject = null;
            //    }
            //}

            ////If they are dragging
            //if (dragging)
            //{
            //    //Get the current screen point
            //    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, draggingObject.transform.position.z);

            //    //Based off of the above point calculate a transform
            //    Vector3 curPosition = camera.ScreenToWorldPoint(curScreenPoint);

            //    //Set the transform to the dragging object
            //    //Lock the Z position
            //    draggingObject.transform.position = new Vector3(curPosition.x, curPosition.y, draggingObject.transform.position.z);
            //}
        }

        public static Puzzle GetPuzzle()
        {
            return GameObject.Find("Puzzle").GetComponent<Puzzle>();
        }

        public virtual void ResetChoices()
        {
        }

        public void ActivateCells(int toActivate)
        {
            if (components[toActivate] != null)
            {
                foreach (GameObject each in components[toActivate])
                {
                    each.GetComponent<CircuitComponent>().activated = true;
                }
            }
        }

        public void DeactivateCells(int toDeactivate)
        {
            if (components[toDeactivate] != null)
            {
                foreach (GameObject each in components[toDeactivate])
                {
                    each.GetComponent<CircuitComponent>().activated = false;
                }
            }
        }

    }
}
