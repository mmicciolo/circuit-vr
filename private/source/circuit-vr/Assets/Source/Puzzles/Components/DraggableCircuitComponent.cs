using Assets.Source.Puzzles.Grids;
using Assets.Source.Puzzles.Grids.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles.Components
{
    class DraggableCircuitComponent : CircuitComponent
    {
        public bool moved = false;
        bool dragging;
        public Vector2 initialPos;
        public Vector3 initialTransformPos;
        Puzzle currentPuzzle;
        FMODUnity.StudioEventEmitter snapToGridEmitter;
        FMODUnity.StudioEventEmitter snapToToolboxEmitter;
        Vector2 mouseDownPosition;

        private void Start()
        {
            initialPos = componentPosition;
            currentPuzzle = GameObject.FindObjectOfType<Puzzle>();

            //GetComponents<CircuitComponent>()[0].enabled = false;
            DisableSwitchComponents();

            gameObject.AddComponent<FMODUnity.StudioEventEmitter>();
            gameObject.AddComponent<FMODUnity.StudioEventEmitter>();
            snapToGridEmitter = GetComponents<FMODUnity.StudioEventEmitter>()[0];
            snapToToolboxEmitter = GetComponents<FMODUnity.StudioEventEmitter>()[1];
            snapToGridEmitter.Event = "event:/SFX/Switch";
            snapToToolboxEmitter.Event = "event:/SFX/Menu Electricity Sounds";
        }

        private void OnMouseDown()
        {
            mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("mouse down");
        }

        private void OnMouseDrag()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z);

            //Based off of the above point calculate a transform
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Set the transform to the dragging object
            //Lock the Z position
            float toCursor = Vector2.Distance(mouseDownPosition, curPosition);

            if (toCursor > 1f)
            {
                dragging = true;
            }

            if (dragging)
            {
                gameObject.transform.position = new Vector3(curPosition.x, curPosition.y, gameObject.transform.position.z);
            }
        }

        private void Update()
        {
        }

        private void OnMouseUp()
        {
            float toAnswer = Vector2.Distance(gameObject.transform.position, PuzzleGrid.GetPuzzleGrid().getCell(currentPuzzle.outputPosition.componentPosition).transform.position);
            float toIni = Vector2.Distance(gameObject.transform.position, initialTransformPos);

            if (dragging)
            {
                if (toAnswer > 2.5f)
                {
                    moved = false;

                    //When not snapped to grid, disable button/switch pushing/flipping
                    DisableSwitchComponents();

                    setComponentToCell(new Vector2(0f, 0f));
                    transform.localPosition = initialTransformPos;

                    snapToToolboxEmitter.Play();
                    Debug.Log("snapped to toolbox");
                }
                else
                {
                    moved = true;

                    //When snapped to the grid, enable the switch/button scripts so that players can push buttons/flip switches
                    EnableSwitchComponents();

                    currentPuzzle.ResetChoices();
                    Vector2 newPos = currentPuzzle.outputPosition.componentPosition;
                    setComponentToCell(newPos);

                    snapToGridEmitter.Play();
                    Debug.Log("snapped to grid");
                }
            }

            dragging = false;
        }

        void DisableSwitchComponents()
        {
            SPSTSwitchCircuitComponent switch1 = gameObject.GetComponent<SPSTSwitchCircuitComponent>();
            if (switch1 != null) switch1.enabled = false;

            PushButtonComponent button1 = gameObject.GetComponent<PushButtonComponent>();
            if (button1 != null) button1.enabled = false;

            PushButtonComponentClosed button2 = gameObject.GetComponent<PushButtonComponentClosed>();
            if (button2 != null) button2.enabled = false;
        }

        void EnableSwitchComponents()
        {
            SPSTSwitchCircuitComponent switch1 = gameObject.GetComponent<SPSTSwitchCircuitComponent>();
            if (switch1 != null) switch1.enabled = true;

            PushButtonComponent button1 = gameObject.GetComponent<PushButtonComponent>();
            if (button1 != null) button1.enabled = true;

            PushButtonComponentClosed button2 = gameObject.GetComponent<PushButtonComponentClosed>();
            if (button2 != null) button2.enabled = true;
        }
    }
}
