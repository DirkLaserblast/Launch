using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

	public AudioClip liftOffSound;
	public GameObject rocket;
	public GameObject rocketCamera;
	public Animator rocketAnimator;
	public GameObject car;
	public dfSprite crosshair;
	public ParticleSystem particles;

	private bool triggerFired;
	
	IEnumerator OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && !triggerFired)
		{
			print ("Trigger Fired");

			car.SetActive(false); //Hide car

			crosshair.enabled = false;

			rocketCamera.SetActive(true); //Switch cameras

			Camera.main.enabled = false;

			rocketAnimator.SetBool("Ignition", true); //Launch rocket
			particles.Play();
			rocket.audio.PlayOneShot(liftOffSound);

			yield return new WaitForSeconds(8);
			//Load credits
			Application.LoadLevel(2);
		}
	}
	
}
