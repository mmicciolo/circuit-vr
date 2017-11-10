using Assets.Source.Interactable_Objects;
using Assets.Source.Puzzles.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles
{
    class Puzzle : InteractablePuzzle
    {
        private bool dragging = false;
        GameObject draggingObject = null;

        private void Start()
        {
            
        }

        private void Update()
        {
            //Wait for the user to press down the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                //Get the main camera in the scene
                Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();

                //Perform a raycast using the mouse position
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                //If we hit something
                if (Physics.Raycast(ray, out hit))
                {
                    //Check to make sure its a component
                    GameObject hitObject = GameObject.Find(hit.transform.name);
                    if(hitObject.GetComponent<CircuitComponent>() != null)
                    {
                        //Start dragging
                        dragging = true;

                        draggingObject = hitObject;

                        Debug.Log("Hit");
                    }
                }
            }

            //If they let go of the mouse button
            if (Input.GetMouseButtonUp(0))
            {
                //See if the object is on the grid
                draggingObject.GetComponent<CircuitComponent>().Snap();

                //They are no longer dragging
                dragging = false;
                draggingObject = null;
            }

            //If they are dragging
            if (dragging)
            {
                //Get the current screen point
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, draggingObject.transform.position.z);

                //Based off of the above point calculate a transform
                Vector3 curPosition = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(curScreenPoint);

                //Set the transform to the dragging object
                //Lock the Z position
                draggingObject.transform.position = new Vector3(curPosition.x, curPosition.y, draggingObject.transform.position.z);
            }
        }
    }
}
