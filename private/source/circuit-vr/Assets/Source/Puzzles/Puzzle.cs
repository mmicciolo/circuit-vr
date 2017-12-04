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
    class Puzzle : MonoBehaviour
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
