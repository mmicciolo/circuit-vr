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

        private void Start()
        {
            initialPos = componentPosition;
            currentPuzzle = GameObject.FindObjectOfType<Puzzle>();
        }

        private void OnMouseDrag()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z);

            //Based off of the above point calculate a transform
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

            //Set the transform to the dragging object
            //Lock the Z position
            gameObject.transform.position = new Vector3(curPosition.x, curPosition.y, gameObject.transform.position.z);
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
                transform.localPosition = initialTransformPos;
            }
            else
            {
                moved = true;

                Vector2 newPos = currentPuzzle.outputPosition.componentPosition;
                setComponentToCell(newPos);
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
