using UnityEngine;
using System.Collections;

public class LifeSupportComplete : MonoBehaviour {

	bool complete = false;
	public dfLabel label;
	public DoorScript door;

	public void PuzzleCompleted() {
		complete = true;
		door.isAirLocked = false;
		label.Text = "All rooms now recieving air.";
	}
}
