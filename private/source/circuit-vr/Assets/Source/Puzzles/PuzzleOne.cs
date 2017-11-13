using Assets.Source.Interactable_Objects;
using Assets.Source.Puzzles.Components;
using Assets.Source.Puzzles.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles
{
    class PuzzleOne : InteractablePuzzle
    {
        PuzzleGrid grid;
        public GameObject[,] components = new GameObject[10, 10];

        private void Start()
        {
            grid = GameObject.Find("Puzzle Grid").GetComponent<PuzzleGrid>();
        }

        private void Update()
        {

        }
    }
}
