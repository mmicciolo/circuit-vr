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

        public void Update()
        {
            if (LevelController.getInstance().CheckDoorCanOpen(puzzleNumber))
            {
                SetGlow(GlowColor.GREEN);
            }
            else
            {
                if (puzzleNumber == LevelController.getInstance().puzzlesCompleted.Count)
                {
                    SetGlow(GlowColor.RED);
                }
                else
                {
                    SetGlow(GlowColor.NONE);
                }
            }
        }

    }
}
