using UnityEngine;
using System.Collections;

public class LightsOut : MonoBehaviour {
	public GameObject lights;
	public GameObject redLights;
	public GameObject Ventillation;
	public GameObject doors;
	public AudioClip ventsPowerDown;
	public GameObject flashlight;
	public GameObject pointlight;
	private float timer = 1f;
	

	void OnTriggerEnter() {
		lights.SetActive (false);
		audio.PlayOneShot(ventsPowerDown);
		Ventillation.SetActive(false);
		foreach (DoorScript dscript in doors.GetComponentsInChildren<DoorScript>())
		{
			dscript.lowPower = true;
		}
		StartCoroutine("TurnOn", timer);
	}

	IEnumerator TurnOn(float t) {
		yield return new WaitForSeconds(t);
		RedLightsOn();
	}

	void RedLightsOn() {
		flashlight.SetActive (true);
		pointlight.SetActive (true);
		redLights.SetActive(true);
	}
}
