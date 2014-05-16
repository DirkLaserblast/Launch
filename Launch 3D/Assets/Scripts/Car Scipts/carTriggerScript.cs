using UnityEngine;
using System.Collections;

public class carTriggerScript : MonoBehaviour {

	public GameObject carCamera;
	public GameObject player;
	public GameObject car;
	public CarUserControl carControlScript;
	public dfSprite crosshair;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.Equals(player))
		{
			car.SetActive(true);
			//Switch to car, disable player
			player.SetActive(false);
			carCamera.SetActive(true);
			carControlScript.enabled = true;
			crosshair.IsVisible = false;

		}
	}
}
