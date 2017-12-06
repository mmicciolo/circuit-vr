using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Puzzles.Components
{
    class LEDCircuitComponent : CircuitComponent
    {
        private Material glowMaterial = null;

        // Use this for initialization
        void Start()
        {
            foreach(Transform t in GetComponentsInChildren<Transform>())
            {
                if(t.name.Equals("polySurface26"))
                {
                    glowMaterial = t.gameObject.GetComponent<Renderer>().material;
                }
            }
            LEDGlowOn(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void LEDGlowOn(bool on)
        {
            if(on)
            {
                glowMaterial.SetFloat("_MKGlowPower", 1.25f);
            }
            else
            {
                glowMaterial.SetFloat("_MKGlowPower", 0.0f);
            }
        }
    }
}

