using UnityEngine;
using System.Collections;

public class DeactivateOnTrigger : MonoBehaviour {

	public GameObject logEntry;
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			logEntry.SetActive(false);
		}
	}
}
