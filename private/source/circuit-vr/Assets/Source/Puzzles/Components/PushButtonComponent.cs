using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using FMODUnity;

namespace Assets.Source.Puzzles.Components
{
    class PushButtonComponent : CircuitComponent
    {
        private Animator animator;
        private StudioEventEmitter press;
        private StudioEventEmitter release;

        public void Start()
        {
            animator = gameObject.GetComponentsInChildren<Animator>()[0];
            press = GetComponents<StudioEventEmitter>()[0];
            release = GetComponents<StudioEventEmitter>()[1];
            GetOriginalMaterial();
        }

        public void OnMouseDown()
        {
            if (enabled)
            {
                animator.Play("button_down");
                press.Play();
            }
        }

        public void OnMouseUp()
        {
            if (enabled)
            {
                release.Play();
                animator.Play("button_up");
            }
        }
    }
}
