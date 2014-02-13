using UnityEngine;
using System.Collections;

public class DrillButtonDown : MonoBehaviour {


	public GameObject DrillObj;
	public GameObject MainDrill;
	public DrillMachineScript DMS;
	public bool inMotion = false;
	private float duration = 0f;
	private Vector3 step = new Vector3(0f, -0.5f, 0f);

	void Start() {
		DMS = MainDrill.GetComponent<DrillMachineScript> ();
	}

	void Update() {
		if (inMotion) {
			DrillObj.transform.Translate(step * Time.deltaTime);
			duration -= Time.deltaTime;
			if(duration <= 0f) {
				inMotion = false;
			}
		}
	}

	void OnMouseDown() {
		if (Input.GetMouseButton (0) && DMS.minigameActive && !inMotion) {
			inMotion = true;
			duration = 1f;
		}
	}

	void OnMouseOver() {
		if(Input.GetMouseButton(0) && DMS.minigameActive) {
			//DrillObj.transform.position = DrillObj.transform.position + step*Time.deltaTime;
		}
	}

	void OnMouseEnter() {
		if(DMS.minigameActive) {
			DMS.SetMouseOver ();
		}
	}

	void OnMouseExit() {
		if(DMS.minigameActive) {
			DMS.SetMouseExit ();
		}
	}

}
