using UnityEngine;
using System.Collections;

public class DrillButtonDown : MonoBehaviour {
	
	public GameObject MoveObj;
	public GameObject SwapButton;
	public float moveDuration = 2f;
	public float brokenMoveDuration = 1f;
	private DrillMove moveScript;
	private DrillButtonSwap swapScript;
	private float timer = 0f;

	void Start() {
		moveScript = MoveObj.GetComponent<DrillMove> ();
		swapScript = SwapButton.GetComponent<DrillButtonSwap> ();
	}

	void Update() {
		if (timer > 0f) {
			timer -= Time.deltaTime;
			if(timer <= 0f) {
				moveScript.InterruptMove(brokenMoveDuration);
				audio.Play();
			}
		}
	}

	void OnMouseDown() {
		if (Input.GetMouseButton (0) && PersistantGlobalScript.minigameActive) {
			if(!swapScript.swapped) {
				moveScript.Move(brokenMoveDuration);
				timer = brokenMoveDuration/2 + Time.deltaTime;
			} else {
				moveScript.Move(moveDuration);
			}
		}
	}
}
