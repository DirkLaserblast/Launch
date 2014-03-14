using UnityEngine;
using System.Collections;

public class EnableObject : MonoBehaviour {

	public GameObject obj;

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			obj.SetActive(true);
		}
	}
}
