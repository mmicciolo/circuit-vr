using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles.Components
{
    class PushButtonComponentClosed : CircuitComponent
    {
        private Animator animator;

        public void Start()
        {
            animator = gameObject.GetComponentsInChildren<Animator>()[0];
            GetOriginalMaterial();
        }

        public void OnMouseDown()
        {
            if (enabled)
            {
                animator.Play("push_button_closed_down");
            }
        }

        public void OnMouseUp()
        {
            if (enabled)
            {
                animator.Play("push_button_closed_up");
            }
        }
    }
}
