using UnityEngine;
using System.Collections;

public class ActivateObjectOnTrigger : MonoBehaviour {

	public GameObject logEntry;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			logEntry.SetActive(true);
		}
	}
}
