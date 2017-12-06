using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Puzzles.Components
{
    class LEDCircuitComponent : CircuitComponent
    {
        private Material glowMaterial = null;
        private float currentGlowPower = 0;

        public bool lighted = false;

        // Use this for initialization
        void Start()
        {
            GetOriginalMaterial();
            foreach(Transform t in GetComponentsInChildren<Transform>())
            {
                if(t.name.Equals("polySurface26"))
                {
                    glowMaterial = t.gameObject.GetComponent<Renderer>().material;
                }
            }
            
        }

        private void Update()
        {
            SetCorrectMaterial();

            if (!activated) lighted = false;

            if (lighted)
            {
                if (currentGlowPower <= 1.8f)
                {
                    currentGlowPower += 0.05f;
                }
            }
            else
            {
                currentGlowPower = 0;
            }
            LEDGlow();
        }

        public void LEDGlow()
        {

                glowMaterial.SetFloat("_MKGlowPower", currentGlowPower);
        }
    }
}

