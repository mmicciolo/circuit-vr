using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Source
{
    class Startup
    {
        [RuntimeInitializeOnLoadMethod]
        static void Startup2()
        {
            //This is where the game begins
            Debug.Log("Starting Game!");

            //Create an instance of the circuit vr game
            new GameObject().AddComponent<CircuitVRGame>();
        }
    }
}
