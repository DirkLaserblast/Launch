using UnityEngine;
using System.Collections;

public class PuzzleComplete : MonoBehaviour {

	public bool complete = false;
	public GameObject obj;
	public GameObject lights;
	public GameObject redLights;
	public GameObject flashlight;
	public GameObject pointlight;
	public GameObject doors;
	
	// Update is called once per frame
	void Awake () {
		complete = true;
		obj.SetActive (true);
		redLights.SetActive (false);
		lights.SetActive (true);
		flashlight.SetActive (false);
		pointlight.SetActive (false);

		

		foreach (DoorScript dscript in doors.GetComponentsInChildren<DoorScript>())
		{
			dscript.lowPower = false;
		}
	}
}
