using UnityEngine;
using System.Collections;

public class PowerPlantTrigger : MonoBehaviour {

	public GameObject powerSceneCamera;
	public Animator cameraAnimator;
	public CarUserControl carControl;
	public GameObject carCamera;
	public RocketScript rocketScript;

	private bool triggerFired = false;

	IEnumerator OnTriggerEnter(Collider Other)
	{
		if (Other.tag == "Player" && !triggerFired)
		{
			triggerFired = true;

			powerSceneCamera.SetActive(true);
			carCamera.SetActive(false);
			carControl.enabled = false;

			cameraAnimator.SetBool("Animating", true);
			yield return new WaitForSeconds(3.5f);

			//Turn main camera and car back on
			carCamera.SetActive(true);
			carControl.enabled = true;
			powerSceneCamera.SetActive(false);

			rocketScript.triggerUnlocked = true;
		}
	}
}
