using UnityEngine;
using System.Collections;

public class PuzzleComplete : MonoBehaviour {

	public bool complete = false;
	public GameObject log;
	public GameObject obj;
	public GameObject lights;
	public GameObject redLights;
	public GameObject flashlight;
	public GameObject pointlight;
	public GameObject doors;
	public GameObject ambientBeepSounds;
	
	// Update is called once per frame
	void Awake () {
		complete = true;
		obj.SetActive (true);
		redLights.SetActive (false);
		lights.SetActive (true);
		flashlight.SetActive (false);
		pointlight.SetActive (false);
		log.SetActive (true);
		ambientBeepSounds.SetActive(true);
		PlayerPrefsX.SetBool("BeepSounds", true);

		foreach (DoorScript dscript in doors.GetComponentsInChildren<DoorScript>())
		{
			dscript.lowPower = false;
		}
	}
}
