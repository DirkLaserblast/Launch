using UnityEngine;
using System.Collections;

public class LightsOff : MonoBehaviour {

	public GameObject lights;
	private DialogueTriggerScript DTS;

	void Start () {
		DTS = GameObject.Find("Dialogue Trigger").GetComponent<DialogueTriggerScript>();
	}

	void OnTriggerEnter() {
		if (DTS.read) {
			lights.SetActive(false);
		}
	}
}
