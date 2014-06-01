using UnityEngine;
using System.Collections;

public class Spacesuit : MonoBehaviour {

	public GameObject carTrigger;
	public GameObject door;
	public GameObject doorLeft;
	public GameObject doorRight;
	public Collider collider;
	public AudioSource doorAudio;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			doorAudio.Play();
			carTrigger.SetActive(true);
			gameObject.SetActive(false);
			doorLeft.SetActive(false);
			doorRight.SetActive(false);
			collider.enabled = false;
		}
	}
}
