using UnityEngine;
using System.Collections;

public class SliderScript : MonoBehaviour {
	public float sliderValue;
	public bool showValue;
	private string sliderString;
	public bool guiOn = false;
	private Vector2 position;

	private GameObject globalScriptsObject;
	private PersistantGlobalScript globalScript;

	public bool triggerOnClick = true;

	// Use this for initialization
	void Start () {
		globalScriptsObject = GameObject.Find("Global Scripts");
		globalScript = globalScriptsObject.GetComponent<PersistantGlobalScript>();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void triggerSlider()
	{
		guiOn = true;
	}

	void OnMouseUp () {
		//SliderTypeObject();

		globalScript.mouseLookEnabled = false;

		guiOn = true;
		position.x = Input.mousePosition.x;
		position.y = Screen.height - Input.mousePosition.y;
	}

	private void OnGUI() {

		Event e = Event.current;
		
		if (guiOn)
		{
			//Time.timeScale = 0;

			//sets the value to a string so it can be displayed and displays it
			sliderString = sliderValue.ToString ();
			if (showValue) GUI.Label (new Rect (position.x + 10, position.y, 100, 100), sliderString);
			//only display if clicked
			if (guiOn) {
				GUI.Box(new Rect(position.x, position.y, 40, 120), "\t");
				sliderValue = GUI.VerticalSlider (new Rect (position.x+14, position.y+14, 50, 100), sliderValue, 1f, 0f);
			}

			if (e.type == EventType.MouseDown && !new Rect(position.x, position.y, 40, 120).Contains(e.mousePosition))
			{
				guiOn = false;
				globalScript.mouseLookEnabled = true;
			}
			
		}
		//else Time.timeScale = 1;


	}
}
