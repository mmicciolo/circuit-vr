using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Puzzles.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Puzzles
{
    class PuzzleK1 : Puzzle
    {
        public DraggableCircuitComponent[] choices;
        private bool close = false;

        private void Start()
        {
            //ActivateCells(0);
        }

        //public override void open()
        //{
        //    base.open();
        //    SceneManager.LoadScene("Scenes/Puzzles/PuzzleK1", LoadSceneMode.Additive);
        //    while(!SceneManager.GetSceneByName("Scenes/Puzzles/PuzzleK1").isLoaded)
        //    {

        //    }
        //    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scenes/Puzzles/PuzzleK1"));
        //    //StartCoroutine(LoadYourAsyncScene());
        //}

        System.Collections.IEnumerator LoadYourAsyncScene()
        {
            // The Application loads the Scene in the background at the same time as the current Scene.
            //This is particularly good for creating loading screens. You could also load the Scene by build //number.
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/Puzzles/PuzzleK1");

            //Wait until the last operation fully loads to return anything
            while (!asyncLoad.isDone)
            {
                yield return SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scenes/Puzzles/PuzzleK1"));
            }
        }

        private void Update()
        {
            if ((choices[0].componentPosition.x == outputPosition.componentPosition.x) && (choices[0].componentPosition.y == outputPosition.componentPosition.y) && !close)
            {
                //Debug.Log("Puzzle solved");
                //Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = false;

                //SceneManager.LoadScene("Kikloma_01");
                close = true;
                close("PuzzleK1");
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
