using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

	public AudioClip liftOffSound;
	public GameObject rocket;
	public GameObject rocketCamera;
	public Animator rocketAnimator;

	//Play audio, switch cameras, start rocket launch animation
	void OnTriggerEnter (Collider other)
	{
		audio.PlayOneShot(liftOffSound);
		rocketCamera.SetActive(true);
		Camera.main.gameObject.SetActive(false);
	}
}
