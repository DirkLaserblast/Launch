using UnityEngine;
using System.Collections;

public class DrillScript : MonoBehaviour {

	/* Script for the actual physical drill.
	 * 
	 * 
	 * 
	 * 
	 * */

	
	public GameObject MainDrill;
	public GameObject DrillButton;
	public GameObject DownButton;
	public bool onRock = false;
	public bool correctDrill = true;
	private DrillButtonDown DownButtonScript;
	private DrillButtonDrill DrillButtonScript;
	private bool drilled = false;

	void Start() {
		DownButtonScript = DownButton.GetComponent<DrillButtonDown>();
		DrillButtonScript = DrillButton.GetComponent<DrillButtonDrill>();

	}

	void Update() {
		if(onRock && correctDrill) {
			if(DrillButtonScript.inMotion) {
				drilled = true;
			}
		}
		if (drilled && onRock) {
			MainDrill.GetComponent<DrillMachineScript>().completed = true;
		}
	}
}
