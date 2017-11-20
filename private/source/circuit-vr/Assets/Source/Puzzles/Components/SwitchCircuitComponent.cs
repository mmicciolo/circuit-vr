using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles.Components
{
    class SwitchCircuitComponent : CircuitComponent
    {
        private Animator animator;
        private AudioSource audio;
        public String lastAnimation = "switch_idle";

        public void Start()
        {
            animator = gameObject.GetComponentsInChildren<Animator>()[0];
            audio = gameObject.GetComponentsInChildren<AudioSource>()[0];
            GetOriginalMaterial();
        }

        public void OnMouseDown()
        {
            audio.Play();
            if(lastAnimation.Equals("switch_idle") || lastAnimation.Equals("switch_down_up"))
            {
                animator.Play("switch_up");
                lastAnimation = "switch_up";
            }
            else if (lastAnimation.Equals("switch_up"))
            {
                animator.Play("switch_up_down");
                lastAnimation = "switch_up_down";
            }
            else if(lastAnimation.Equals("switch_up_down"))
            {
                animator.Play("switch_down");
                lastAnimation = "switch_down";
            }
            else if(lastAnimation.Equals("switch_down"))
            {
                animator.Play("switch_down_up");
                lastAnimation = "switch_down_up";
            }
        }
    }
}
