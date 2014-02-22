using UnityEngine;
using System.Collections;

public class LightsOff : MonoBehaviour {

	public GameObject lights;

	void OnTriggerEnter() {
		lights.SetActive(false);
	}
}
