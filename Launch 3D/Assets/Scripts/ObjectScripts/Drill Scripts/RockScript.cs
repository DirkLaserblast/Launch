using UnityEngine;
using System.Collections;

public class RockScript : MonoBehaviour {

	/* The rock isn't really a collider, but rather just a trigger that
	 * will stop the drill from moving down and that will check if
	 * the drill is currently resting on top of the rock.
	 *  */

	public GameObject MainDrill;
	public GameObject DrillButton;

	void Start() {
		transform.collider.isTrigger = true;
	}

	void OnTriggerEnter(Collider trigger) {
		if (MainDrill.GetComponent<DrillMachineScript> ().minigameActive) {
			trigger.gameObject.GetComponent<DrillScript> ().onRock = true;
			DrillButton.GetComponent<DrillButtonDown>().duration = 0f;
		}
	}

	void OnTriggerExit(Collider trigger) {
		if (MainDrill.GetComponent<DrillMachineScript> ().minigameActive) {
			trigger.gameObject.GetComponent<DrillScript> ().onRock = false;
		}
	}


}
