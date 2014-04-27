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
	public AudioClip log1;
	//public AudioClip music;
	public float timeout;
	public GameObject player;
	private bool playing = false;
	private float timer = 1f;
	private float logtimer = 4f;
	
	void Update() {
		if (playing) {
			transform.position = player.transform.position;
		}
	}

	void OnTriggerEnter() {
		playing = true;
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
		flashlight.SetActive(true);
		pointlight.SetActive(true);
		redLights.SetActive(true);
		StartCoroutine("WaitToPlayLog", logtimer);
	}
	
	IEnumerator WaitToPlayLog(float t) {
		yield return new WaitForSeconds(t);
		PlayLog ();
	}
	
	void PlayLog() {
		audio.PlayOneShot (log1);
		//audio.PlayOneShot (music);
		StartCoroutine("TimeOut", timeout);
	}
	
	IEnumerator TimeOut(float t) {
		yield return new WaitForSeconds(t);
		gameObject.SetActive (false);
	}
}
