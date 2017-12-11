using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using FMODUnity;

namespace Assets.Source.Puzzles.Components
{
    class SPSTSwitchCircuitComponent : CircuitComponent
    {
        private Animator animator;
        private StudioEventEmitter press;
        public String lastAnimation = "switch_idle";

        public void Start()
        {
            animator = gameObject.GetComponentsInChildren<Animator>()[0];
            press = GetComponents<StudioEventEmitter>()[0];
            GetOriginalMaterial();
        }

        public void OnMouseDown()
        {
            if (enabled)
            {
                press.Play();
                if (lastAnimation.Equals("switch_idle") || lastAnimation.Equals("spst_up"))
                {
                    animator.Play("spst_down");
                    lastAnimation = "spst_down";
                }
                else if (lastAnimation.Equals("spst_down"))
                {
                    animator.Play("spst_up");
                    lastAnimation = "spst_up";
                }
            }
        }
    }
}
