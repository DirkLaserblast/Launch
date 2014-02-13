using UnityEngine;
using System.Collections;

public class RockScript : MonoBehaviour {

	/* The rock isn't really a collider, but rather just a trigger that
	 * will stop the drill from moving down and that will check if
	 * the drill is currently resting on top of the rock.
	 *  */



	void Start() {
		transform.collider.isTrigger = true;
	}

	void OnTriggerEnter(Collider trigger) {
		trigger.gameObject.GetComponent<DrillScript>().onRock = true;
	}

	void OnTriggerExit(Collider trigger) {
		trigger.gameObject.GetComponent<DrillScript>().onRock = false;
	}


}
