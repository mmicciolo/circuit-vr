using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source.Puzzles
{
    public class PuzzleController
    {
        public bool[] PuzzlesComplete = new bool[12];

        public PuzzleController()
        {
            for(int i = 0; i < PuzzlesComplete.Length; i++)
            {
                PuzzlesComplete[i] = false;
            }
        }

        public void SetComplete(int puzzleNumber)
        {
            PuzzlesComplete[puzzleNumber] = true;
        }

        public bool GetComplete(int puzzleNumber)
        {
            return PuzzlesComplete[puzzleNumber];
        }

    }
}
