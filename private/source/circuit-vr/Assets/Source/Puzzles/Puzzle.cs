using Assets.Source.Interactable_Objects;
using Assets.Source.Puzzles.Components;
using Assets.Source.Puzzles.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using FMODUnity;

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
        protected List<int> activatedGroups;

        public Text infoText;

        protected double endDuration = 0;
		protected Stopwatch stopwatch = null;
        protected int stepsSinceCompletion;
        protected bool completed = false;
        protected string puzzleName;

        private StudioEventEmitter puzzleSound;

        public DraggableCircuitComponent[] choices;

		protected int puzzleNumber = 0;

        private StudioEventEmitter buzzSound;

        private void Start()
        {
			InitPuzzle(puzzleNumber);
        }

		protected void InitPuzzle(int number)
        {
            puzzleGrid = PuzzleGrid.GetPuzzleGrid();

            camera = GameObject.Find("Main Camera").GetComponent<Camera>();

            infoText.transform.position = new Vector3(13f, -5f, 9f);

            activatedGroups = new List<int>();

            stepsSinceCompletion = 0;

			puzzleNumber = number;

            puzzleSound = gameObject.AddComponent<StudioEventEmitter>();
            puzzleSound.Event = "event:/SFX/Puzzle Start";
            puzzleSound.Play();

            buzzSound = gameObject.AddComponent<StudioEventEmitter>();
            puzzleSound.Event = "event:/SFX/Menu Tone 2";
        }

    public Camera GetCamera()
        {
            return camera;
        }

        private void Update()
        {
            CheckCompletion();
        }

		protected void MarkCompleted() {
			completed = true;
			LevelController.getInstance ().SetCompleted (puzzleNumber);
		}

        protected void CheckCompletion()
        {
            if (completed)
            {
                if (stepsSinceCompletion == 0)
                {
                    stopwatch = new Stopwatch();
                    stopwatch.Start();
                }

                int elapsed = (int) stopwatch.Elapsed.TotalSeconds;
                //UnityEngine.Debug.Log(elapsed);
                if ((elapsed == endDuration) || (endDuration == 0))
                {
                    ClosePuzzle();
                }
                else
                {
                    AnimateEnd();
                    stepsSinceCompletion++;
                }
            }
        }
        
        protected virtual void AnimateEnd()
        {
            //UnityEngine.Debug.Log("animate called");
            if ((stopwatch.ElapsedMilliseconds >= 500) && (stopwatch.ElapsedMilliseconds < 600))
            {
                foreach (int c in activatedGroups)
                {
                    DeactivateCells(c);
                }
            }

            if ((stopwatch.ElapsedMilliseconds >= 600) && (stopwatch.ElapsedMilliseconds < 700))
            {
                foreach (int c in activatedGroups)
                {
                    ActivateCells(c);
                }

                //if (!buzzSound.IsPlaying()) buzzSound.Play();
            }

            if ((stopwatch.ElapsedMilliseconds >= 700) && (stopwatch.ElapsedMilliseconds < 800))
            {
                foreach (int c in activatedGroups)
                {
                    DeactivateCells(c);
                }
            }

            if (stopwatch.ElapsedMilliseconds >= 800)
            {
                foreach (int c in activatedGroups)
                {
                    ActivateCells(c);
                }
                //if (!buzzSound.IsPlaying()) buzzSound.Play();
            }
        }

        protected void ClosePuzzle()
        {
            LevelController.getInstance().closePuzzle(puzzleName);
        }

        public static Puzzle GetPuzzle()
        {
            return GameObject.Find("Puzzle").GetComponent<Puzzle>();
        }

        public void ResetChoices()
        {
            Vector2 cell = new Vector2(0f, 0f);
            if (choices.Length > 0)
            {
                for (int i = 0; i < choices.Length; i++)
                {
                    choices[i].moved = false;
                    choices[i].attachedComponent.setComponentToCell(cell);
                    choices[i].transform.localPosition = choices[i].initialTransformPos;
                    if (components[choices[i].attachedComponent.group] != null)
                    {
                        components[choices[i].attachedComponent.group].Remove(choices[i].gameObject);
                    }
                }
            }
        }

        protected void DisableDragging()
        {
            if (choices.Length > 0)
            {
                for (int i = 0; i < choices.Length; i++)
                {
                    choices[i].dragEnabled = false;
                }
            }
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

            if (!activatedGroups.Contains(toActivate) && !completed)
            {
                activatedGroups.Add(toActivate);
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

            if (activatedGroups.Contains(toDeactivate) && !completed)
            {
                activatedGroups.Remove(toDeactivate);
            }
        }

    }
}
