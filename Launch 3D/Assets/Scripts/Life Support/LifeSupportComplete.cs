using UnityEngine;
using System.Collections;

public class LifeSupportComplete : MonoBehaviour {

	bool complete = false;
	public dfLabel label;
	public GameObject doors;

	public void PuzzleCompleted() {
		complete = true;
		//doors.SetActive = true;
		label.Text = "All rooms now recieving air.";
	}
}
