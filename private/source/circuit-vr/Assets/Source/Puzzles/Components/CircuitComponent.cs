﻿using Assets.Source.Puzzles.Grids;
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
        public int x;
        public int y;
        public bool activated = false;

        private void Start()
        {

            PuzzleGrid grid = GameObject.Find("Puzzle Grid").GetComponent<PuzzleGrid>();
            transform.position = grid.transform.position + new Vector3(x * 1.5f + 0.75f, y * 1.5f - 0.75f, 0);
            //transform.localScale = new Vector2(2f, 2f);

            PuzzleOne puzzleOne = GameObject.Find("PuzzleOne").GetComponent<PuzzleOne>();
            puzzleOne.components[x, y] = gameObject;
        }

        private void Update()
        {

        }

        public void Snap()
        {
            //Get the grid
            PuzzleGrid grid = GameObject.Find("Puzzle Grid").GetComponent<PuzzleGrid>();

            float distance = 1000f;
            PuzzleCell closestCell = null;
            foreach(PuzzleCell cell in grid.gridCells)
            {
                float cellDistance = Vector2.Distance(new Vector2(transform.position.x - 1.25f, transform.position.y + 1.25f), new Vector2(cell.transform.position.x, cell.transform.position.y));
                if (cellDistance < distance)
                {
                    closestCell = cell;
                    distance = cellDistance;
                }
            }

            if(closestCell != null && distance <= 5f)
            {
                Vector3 pos = closestCell.transform.position;
                pos.x += 1.25f; pos.y -= 1.25f;
                transform.position = pos;
            }
        }
    }
}
