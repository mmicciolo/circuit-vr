using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit23 : Puzzle
    {
        public DraggableCircuitComponent[] choices;

        private void Start()
        {
            //ActivateCells(0);
        }

        private void Update()
        {
            if ((choices[0].componentPosition.x == outputPosition.componentPosition.x) && (choices[0].componentPosition.y == outputPosition.componentPosition.y))
            {
                Debug.Log("Puzzle solved");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                SceneManager.LoadScene("Kikloma_01");
            }
        }

        override
        public void ResetChoices()
        {
            Vector2 cell = new Vector2(0f, 0f);
            for (int i = 0; i < choices.Length; i++)
            {
                choices[i].moved = false;
                choices[i].setComponentToCell(cell);
                choices[i].transform.localPosition = choices[i].initialTransformPos;
            }
        }
    }
}
