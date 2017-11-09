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
        public void open()
        {
            SceneManager.LoadScene("Scenes/testpuzzle", LoadSceneMode.Single);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("testpuzzle"));
        }
    }
}
