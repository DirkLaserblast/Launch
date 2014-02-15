using UnityEngine;
using System.Collections;

public class RockScript : MonoBehaviour {
	
	/*
	 * A trigger to check if the drill has hit the rock
	 * */

	public GameObject drillMove;
	private DrillMove drillMoveScript;

	void Start() {
		transform.collider.isTrigger = true;
		drillMoveScript = drillMove.GetComponent<DrillMove> ();
	}

	void OnTriggerEnter(Collider trigger) {
		if (PersistantGlobalScript.minigameActive) {
			//Some kind of rock flag needs to be set
			drillMoveScript.Stop();
		}
	}

	void OnTriggerExit(Collider trigger) {
		if (PersistantGlobalScript.minigameActive) {
			//Some kind of rock flag needs to be unset
		}
	}


}
