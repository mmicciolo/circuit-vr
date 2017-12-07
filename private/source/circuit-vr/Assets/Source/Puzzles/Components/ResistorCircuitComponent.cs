using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles.Components
{
    public enum ResistorColor
    {
        RED,
        GREEN,
        BLUE,
		YELLOW
    }

    class ResistorCircuitComponent : CircuitComponent
    {
        public ResistorColor resistorColor;

        public void Start()
        {
            GetOriginalMaterial();
            SetResistorColor(resistorColor);
        }

        public GameObject GetResistorModel()
        {
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t.name.Equals("polySurface4"))
                {
                    return t.gameObject;
                }
            }
            return null;
        }

        public void SetResistorColor(ResistorColor color)
        {
            switch(color)
            {
                case ResistorColor.RED:
                    GetResistorModel().GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                    break;
                case ResistorColor.GREEN:
                    GetResistorModel().GetComponent<Renderer>().material.color = new Color(0, 255, 0);
                    break;
                case ResistorColor.BLUE:
                    GetResistorModel().GetComponent<Renderer>().material.color = new Color(0, 0, 255);
                    break;
				case ResistorColor.YELLOW:
				GetResistorModel ().GetComponent<Renderer> ().material.color = new Color (255f, (float)0.92*255, (float)0.016*255);
					break;
                default:
                    break;
            }
        }
    }
}
