using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Source
{
    [InitializeOnLoad]
    class Startup
    {
        static Startup()
        {
            Debug.Log("Up and running");
        }
    }
}
