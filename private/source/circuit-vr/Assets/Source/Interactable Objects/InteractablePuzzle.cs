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
        public string puzzleName;

        public virtual void open()
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            LevelController.getInstance().RememberPosition();

                            SceneManager.LoadScene(puzzleName, LoadSceneMode.Single);

        }

    }
}
