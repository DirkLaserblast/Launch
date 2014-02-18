using UnityEngine;
using System.Collections;

public class TweenScript : MonoBehaviour {

	public bool down;
	public bool mouseInRegion;
	
	// Update is called once per frame
	void Update () {
		MouseInRegion ();
	}

	private void MouseInRegion() {
		print (Camera.main.ScreenToViewportPoint(Input.mousePosition));
		if (Camera.main.ScreenToViewportPoint(Input.mousePosition).y > 0.7) {
			mouseInRegion = true;
		} else {
			mouseInRegion = false;
		}
	}

	public void Something() {

	}
}
