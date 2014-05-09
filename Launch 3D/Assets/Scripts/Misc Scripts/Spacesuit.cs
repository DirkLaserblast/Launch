using UnityEngine;
using System.Collections;

public class Spacesuit : MonoBehaviour {

	public GameObject carTrigger;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			carTrigger.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
