using UnityEngine;
using System.Collections;

public class OnOffObject : MonoBehaviour {
	private bool toggleOnOff = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (toggleOnOff) {
			Camera.main.backgroundColor = Color.grey;
		}
		else {
			Camera.main.backgroundColor = Color.magenta;
		}
	}

	void OnMouseUp () {
		toggleOnOff = !toggleOnOff;
	}
}
