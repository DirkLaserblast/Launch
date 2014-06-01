using UnityEngine;
using System.Collections;

public class ReticleScript : MonoBehaviour {
	public GameObject hand;
	public GameObject crosshair;
	public int maxDistance;
	public Transform player;

	void OnMouseOver() {
		float distance = Mathf.Abs ((transform.position - player.position).magnitude);
		print (distance);
		if (distance < maxDistance && !PersistantGlobalScript.minigameActive) {
			hand.SetActive(true);
			crosshair.SetActive (false);
		} else {
			hand.SetActive(false);
			crosshair.SetActive (true);
		}
	}

	void OnMouseExit() {
		hand.SetActive(false);
		crosshair.SetActive (true);
	}
}
