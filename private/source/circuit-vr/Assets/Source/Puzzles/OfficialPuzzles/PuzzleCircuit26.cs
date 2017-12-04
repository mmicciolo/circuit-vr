using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleCircuit26 : Puzzle
    {
        public SwitchCircuitComponent circuitSwitch;
        public CircuitComponent capacitor;

        int step;

        private void Start()
        {
            step = 0;
        }

        private void Update()
        {
            double time = 10 - Math.Floor((double)step / 60);
            if (step == 1)
            {
                circuitSwitch.animator.Play("switch_down");
                circuitSwitch.lastAnimation = "switch_down";
            }

            capacitor.gameObject.GetComponent<DisplayInfo>().notation = "Capacitor overload in " + time;

            if (time == 0)
            {
                capacitor.gameObject.GetComponent<DisplayInfo>().notation = "Capacitor exploded";
                //LevelController.getInstance().closePuzzle("PuzzleCircuit26");
            } else
            {
                step += 1;
                        if (circuitSwitch.lastAnimation == "switch_up")
            {
                Debug.Log("Puzzle solved");
                //LevelController.getInstance().closePuzzle("PuzzleCircuit26");
            }
            }



        }

    }
}
