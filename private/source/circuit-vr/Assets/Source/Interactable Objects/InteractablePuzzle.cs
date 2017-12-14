using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Interactable_Objects
{
    enum GlowColor
    {
        NONE,
        RED,
        GREEN,
        BLUE
    }

    class InteractablePuzzle : MonoBehaviour
    {
		public int puzzleNumber;
        string sceneName;

        private GameObject tablet = null;
        private Material tabletMaterial = null;
        private Material tabletGlowRed = null;
        private Material tabletGlowGreen = null;
        private Material tabletGlowBlue = null;

        public void Start()
        {
            tablet = gameObject.transform.Find("pCube1").gameObject;
            tabletMaterial = tablet.GetComponent<Renderer>().material;
            tabletGlowRed = Resources.Load<Material>("tablet_glow_red") as Material;
            tabletGlowGreen = Resources.Load<Material>("tablet_glow_green") as Material;
            tabletGlowBlue = Resources.Load<Material>("tablet_glow_blue") as Material;
        }

        public virtual void open()
        {
			sceneName = LevelController.getInstance ().puzzleOrder [puzzleNumber];
			Debug.Log (sceneName);
            LevelController.getInstance().openPuzzle(sceneName);
        }

        public void SetGlow(GlowColor color)
        {
            switch(color)
            {
                case GlowColor.NONE:
                    tablet.GetComponent<Renderer>().material = tabletMaterial;
                    break;
                case GlowColor.RED:
                    tablet.GetComponent<Renderer>().material = tabletGlowRed;
                    break;
                case GlowColor.GREEN:
                    tablet.GetComponent<Renderer>().material = tabletGlowGreen;
                    break;
                case GlowColor.BLUE:
                    tablet.GetComponent<Renderer>().material = tabletGlowBlue;
                    break;
                default:
                    break;
            }
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
