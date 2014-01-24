using UnityEngine;
using System.Collections;

public class ToggleObject : MonoBehaviour {
	
	/*
	*	ToggledObject is dragged in via the GUI.
	*	toggle() calls the ToggledObject's own method.
	*	This code (and ToggledObject's) can be copied and moved into other scripts
	*/
	
	public bool toggled = false;
	public ToggledObject toggledObject;
	
	void OnMouseDown() {
		toggle();
	}
	
	
	void toggle() {
		toggledObject.toggle();
		if (toggled) {
			toggled = false;
		} else {
			toggled = true;
		}
	}
}
