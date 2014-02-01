using UnityEngine;
using System.Collections;

public class Unlock : MonoBehaviour {

	public GameObject LO;

	void Update () {
	
	}

	void OnMouseDown() {
		LO.GetComponent<DoorScript>().locked = false;
		LO.renderer.enabled = false;
	}
}
