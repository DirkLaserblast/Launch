using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {
	public double timeLeft = 1f;
	private double baseTime = 1f;
	private bool flipped = false;
	public bool started = false;
	public GameObject lockedDoor;
	public GameObject unlockedDoor;
	public AudioClip leverSound;
	private DoorScript lockedDoorScript;
	private DoorScript unlockedDoorScript;


	void Start () {
		lockedDoorScript = lockedDoor.GetComponent<DoorScript> ();
		unlockedDoorScript = unlockedDoor.GetComponent<DoorScript> ();
	}
	
	void Update () {
		if (started && timeLeft > 0) {
			timeLeft -= Time.deltaTime;
			if(!flipped) {
				transform.Rotate(0, 0, 60*Time.deltaTime);
			} else {
				transform.Rotate(0, 0, -60*Time.deltaTime);
			}
		}
		if (timeLeft <= 0) {
			timeLeft = baseTime;
			started = false;
			Toggle();
		}
	}
	
	void OnMouseDown() {
		started = true;
	}

	void Toggle() {
		audio.PlayOneShot(leverSound);
		if (flipped) {
			flipped = false;
			lockedDoorScript.isLocked = true;
			unlockedDoorScript.isLocked = false;
		} else {
			flipped = true;
			lockedDoorScript.isLocked = false;
			unlockedDoorScript.isLocked = true;
		}
	}
}
