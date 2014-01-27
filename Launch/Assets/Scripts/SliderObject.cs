using UnityEngine;
using System.Collections;

public class SliderObject : MonoBehaviour {
	private float sliderValue;
	private string sliderString;
	private bool guiOn = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		//SliderTypeObject();
		guiOn = true;
	}

	private void OnGUI() {
		//sets the value to a string so it can be displayed and displays it
		sliderString = sliderValue.ToString ();
		GUI.Label (new Rect (0, 0, 100, 100), sliderString);
		//only display if clicked
		if (guiOn) {
			GUI.Box(new Rect(86, 90, 40, 120), "\t");
			sliderValue = GUI.VerticalSlider (new Rect (100, 100, 50, 100), sliderValue, 1f, 0f);
		}
	}
}
