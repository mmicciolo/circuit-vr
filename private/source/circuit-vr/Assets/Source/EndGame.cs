using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class EndGame : MonoBehaviour {
	protected Stopwatch stopwatch = null;

	// Use this for initialization
	void Start () {
		stopwatch = new Stopwatch ();
		stopwatch.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((int)stopwatch.Elapsed.TotalSeconds == 10) {
			SceneManager.LoadScene("Start Screen");
		}
	}
}
