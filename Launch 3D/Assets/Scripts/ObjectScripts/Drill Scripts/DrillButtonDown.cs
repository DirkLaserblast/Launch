using UnityEngine;
using System.Collections;

public class DrillButtonDown : MonoBehaviour {
	
	public GameObject MoveObj;
	public float drillDuration = 2f;
	private DrillMove moveScript;
	
	void Start() {
		moveScript = MoveObj.GetComponent<DrillMove> ();
	}
	
	void OnMouseDown() {
		if (Input.GetMouseButton (0) && PersistantGlobalScript.minigameActive) {
			moveScript.Move(drillDuration);
		}
	}
}
