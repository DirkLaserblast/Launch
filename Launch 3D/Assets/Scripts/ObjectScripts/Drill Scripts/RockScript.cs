using UnityEngine;
using System.Collections;

public class RockScript : MonoBehaviour {
	
	/*
	 * A trigger to check if the drill has hit the rock
	 * */

	public GameObject drillMove;
	public bool onRock = false;
	private DrillMove drillMoveScript;

	void Start() {
		transform.collider.isTrigger = true;
		drillMoveScript = drillMove.GetComponent<DrillMove> ();
	}

	void OnTriggerEnter(Collider trigger) {
		if (PersistantGlobalScript.minigameActive) {
			onRock = true;
			drillMoveScript.Stop();
		}
	}

	void OnTriggerExit(Collider trigger) {
		if (PersistantGlobalScript.minigameActive) {
			onRock = false;
		}
	}


}
