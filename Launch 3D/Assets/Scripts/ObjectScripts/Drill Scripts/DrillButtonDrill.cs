using UnityEngine;
using System.Collections;

public class DrillButtonDrill : MonoBehaviour {

	public GameObject SmallDrill;
	public GameObject BigDrill;
	public float drillDuration = 2f;
	private DrillScript drillScript;
	
	void Start() {
		drillScript = BigDrill.GetComponent<DrillScript> ();
	}
	
	void OnMouseDown() {
		if (Input.GetMouseButton (0) && PersistantGlobalScript.minigameActive) {
			audio.Play ();
			drillScript.Drill(drillDuration);
		}
	}

	public void swapDrills() {
		//simple version, will do fancy transition later on.
		if (BigDrill.activeSelf) {
			BigDrill.SetActive(false);
			SmallDrill.SetActive(true);
			drillScript = SmallDrill.GetComponent<DrillScript>();
		} else {
			SmallDrill.SetActive(false);
			BigDrill.SetActive(true);
			drillScript = BigDrill.GetComponent<DrillScript>();
		}
	}
}
