using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Interactable_Objects
{
    class InteractablePuzzle : MonoBehaviour
    {
        public string sceneName;

        public virtual void open()
        {
            LevelController.getInstance().openPuzzle(sceneName);
        }

        //private void OnPuzzleLoaded(Scene scene, LoadSceneMode mode)
        //{
        //    //Remove the callback
        //    SceneManager.sceneLoaded -= OnPuzzleLoaded;

        //    //Disable the main scene objects
        //    foreach (GameObject obj in SceneManager.GetSceneByName(openScene).GetRootGameObjects())
        //    {
        //        if (!obj.active) { disabledObject.Add(obj); } else { obj.SetActive(false); }
        //    }

        //    //Enable the cursor
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;

        //    //Set the scene as active
        //    SceneManager.SetActiveScene(scene);
        //}

        //public void close(string name)
        //{
        //    //Add a callback for when the unloading completes
        //    SceneManager.sceneUnloaded += OnPuzzleUnloaded;

        //    //Load the scene
        //    SceneManager.UnloadScene(name);
        //}

        //private void OnPuzzleUnloaded(Scene scene)
        //{
        //    //Remove the callback
        //    SceneManager.sceneUnloaded -= OnPuzzleUnloaded;

        //    //Enable the main scene objects
        //    foreach (GameObject obj in SceneManager.GetSceneByName(openScene).GetRootGameObjects())
        //    {
        //        if (!disabledObject.Contains(obj)) { obj.SetActive(true); }
        //    }

        //    //Clear the list
        //    disabledObject.Clear();

        //    //Disable the cursor
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;

        //    //Set the scene as active
        //    SceneManager.SetActiveScene(SceneManager.GetSceneByName(openScene));
        //}

        public void Update()
        {

        }
        //public string puzzleName;

        //public virtual void open()
        //{
        //    Assets.Source.Player.FirstPersonPlayer firstPersonPlayer = GameObject.FindObjectOfType<Assets.Source.Player.FirstPersonPlayer>();

        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;

        //    LevelController.getInstance().playerPosition = firstPersonPlayer.transform.position;

        //                    SceneManager.LoadScene(puzzleName, LoadSceneMode.Single);

        //}

    }
}
