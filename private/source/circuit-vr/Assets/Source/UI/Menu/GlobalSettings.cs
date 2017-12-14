using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.UI.Menu
{
    class GlobalSettings : MonoBehaviour
    {
        public static GlobalSettings Instance;

        public float volume;
        public bool muted;

        public void Awake()
        {
            if(Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
                volume = 0.5f;
                muted = false;
            }
            else if(Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
