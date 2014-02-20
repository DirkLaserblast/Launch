using UnityEngine;
using System.Collections;

public class DrillButtonSwap : MonoBehaviour {

	/*
	 * Will activate an object that you can drop an item into.
	 * Also calls the function to swap the drills.
	 * */

	public GameObject Receptical;
	public GameObject DrillButton;
	public bool swapped = false;
	private ItemReceive itemScript;
	private DrillButtonDrill drillScript;

	void Start() {
		drillScript = DrillButton.GetComponent<DrillButtonDrill> ();
		itemScript = Receptical.GetComponent<ItemReceive> ();
		Receptical.SetActive(false);
	}

	void Update() {
		if (!swapped && itemScript.received) {
			drillScript.swapDrills();
			swapped = true;
			Receptical.SetActive(false);
		}
	}

	void OnMouseDown() {
		if (PersistantGlobalScript.minigameActive) {
			if (!itemScript.received) {
				Receptical.SetActive (true);
			} else {
				Receptical.SetActive (false);
			}
		}
	}
}
