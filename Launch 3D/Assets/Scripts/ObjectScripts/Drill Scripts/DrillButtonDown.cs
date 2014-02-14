using UnityEngine;
using System.Collections;

public class DrillButtonDown : MonoBehaviour {


	public GameObject DrillObj;
	public GameObject MainDrill;
	public bool inMotion = false;
	public float duration = 0f;
	private bool down = true;
	private DrillMachineScript DMS;
	private Vector3 step = new Vector3(0f, -0.2f, 0f);
	private Vector3 anchor;

	void Start() {
		DMS = MainDrill.GetComponent<DrillMachineScript> ();
		anchor = DrillObj.transform.position;
	}

	void Update() {
		if (inMotion) {
			if(down) {
				DrillObj.transform.Translate(step * Time.deltaTime, Space.World);
				duration -= Time.deltaTime;
				if(duration <= 0f) {
					inMotion = false;
					down = false;
				}
			} else {
				//DrillObj.transform.Translate(step * Time.deltaTime, Space.World);
				DrillObj.transform.position = Vector3.MoveTowards(DrillObj.transform.position, anchor, Time.deltaTime);
				duration -= Time.deltaTime;
				if(duration <= 0f) {
					inMotion = false;
					down = true;
				}
			}
		}
	}

	void OnMouseDown() {
		if (Input.GetMouseButton (0) && DMS.minigameActive && !inMotion) {
			inMotion = true;
			duration = 0.6f;
		}
	}

	void OnMouseOver() {
		if(Input.GetMouseButton(0) && DMS.minigameActive) {
			//DrillObj.transform.position = DrillObj.transform.position + step*Time.deltaTime;
		}
	}

	void OnMouseEnter() {
		if(DMS.minigameActive) {
			DMS.SetMouseOver();
		}
	}

	void OnMouseExit() {
		if(DMS.minigameActive) {
			DMS.SetMouseExit();
		}
	}

}
