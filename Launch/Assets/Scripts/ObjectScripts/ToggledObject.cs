using UnityEngine;
using System.Collections;

public class ToggledObject : MonoBehaviour {

	public bool toggled = false;

	/*
	*	ToggledObject is dragged into toggleObject via the GUI.
	*	toggle() called by the *other* script
	*	This code (and ToggleObject's) can be copied and moved into other scripts
	*/


	void Update () {
		if (toggled) {
			//Toggled action goes here
		}
	}

	public void toggle() {
		if (toggled) {
			toggled = false;
		} else {
			toggled = true;
		}
	}
}
