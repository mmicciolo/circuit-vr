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
        public Vector2 initialPos;
        public Vector3 initialTransformPos;
        Puzzle currentPuzzle;
        FMODUnity.StudioEventEmitter snapEmitter;
        FMODUnity.StudioEventEmitter wrongEmitter;
        Vector2 mouseDownPosition;

        private void Start()
        {
            initialPos = componentPosition;
            currentPuzzle = GameObject.FindObjectOfType<Puzzle>();
            gameObject.AddComponent<FMODUnity.StudioEventEmitter>();
            gameObject.AddComponent<FMODUnity.StudioEventEmitter>();

            GetComponents<CircuitComponent>()[0].enabled = false;

            snapEmitter = GetComponents<FMODUnity.StudioEventEmitter>()[0];
            wrongEmitter = GetComponents<FMODUnity.StudioEventEmitter>()[1];
            snapEmitter.Event = "event:/SFX/Switch";
            wrongEmitter.Event = "event:/SFX/Menu Electricity Sounds";
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
                gameObject.transform.position = new Vector3(curPosition.x, curPosition.y, gameObject.transform.position.z);
            }
        }

        private void Update()
        {
        }

        private void OnMouseUp()
        {
            //Snap();

            //float toAnswer = Vector2.Distance(gameObject.transform.position, PuzzleGrid.GetPuzzleGrid().getCell(currentPuzzle.outputPosition.componentPosition).transform.position);
            //float toIni = Vector2.Distance(gameObject.transform.position, PuzzleGrid.GetPuzzleGrid().getCell(initialPos).transform.position);
            float toAnswer = Vector2.Distance(gameObject.transform.position, PuzzleGrid.GetPuzzleGrid().getCell(currentPuzzle.outputPosition.componentPosition).transform.position);
            float toIni = Vector2.Distance(gameObject.transform.position, initialTransformPos);
            if (toAnswer > 2.5f)
            {
                moved = false;
                GetComponents<CircuitComponent>()[0].enabled = false;
                setComponentToCell(new Vector2(0f, 0f));
                //transform.localPosition = initialTransformPos;
                transform.localPosition = initialTransformPos;
                wrongEmitter.Play();
                Debug.Log("played event:/SFX/Menu Electricity Sounds sound");
            }
            else
            {
                moved = true;
                GetComponents<CircuitComponent>()[0].enabled = true;
                currentPuzzle.ResetChoices();
                Vector2 newPos = currentPuzzle.outputPosition.componentPosition;
                setComponentToCell(newPos);
                snapEmitter.Play();
                Debug.Log("played event:/SFX/");

            }

            //float toAnswer = Vector2.Distance(gameObject.transform.position, PuzzleGrid.GetPuzzleGrid().getCell(currentPuzzle.outputPosition.componentPosition).transform.position);
            //float toIni = Vector2.Distance(gameObject.transform.position, PuzzleGrid.GetPuzzleGrid().getCell(initialPos).transform.position);
            //if (toIni < toAnswer)
            //{
            //    moved = false;
            //    setComponentToCell(initialPos);
            //}
            //else
            //{
            //    //currentPuzzle.ResetChoices();
            //    moved = true;

            //    Vector2 newPos = currentPuzzle.outputPosition.componentPosition;
            //    setComponentToCell(newPos);
            //}
        }


    }
}
