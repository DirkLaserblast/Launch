using UnityEngine;
using System.Collections;

public class Spacesuit : MonoBehaviour {

	public GameObject carTrigger;
	public GameObject door;
	public Animator doorAnimation;
	public DoorScript lockDoor;
//	public Collider collider;
	public AudioSource doorAudio;
	public int maxDistance;
	public Transform player;
	public GameObject hand;
	public GameObject reticle;

	void OnMouseUp() {			
		float distance = Mathf.Abs((transform.position - player.position).magnitude);
		if (distance < maxDistance) {
			TakeSpacesuit();
		}
	}



	void TakeSpacesuit() {
		doorAudio.Play();
		carTrigger.SetActive(true);
		gameObject.SetActive(false);
		doorAnimation.SetBool("Open", true);
		reticle.SetActive (true);
		hand.SetActive (false);
//		collider.enabled = false;
		lockDoor.locked = true;
	}
}
