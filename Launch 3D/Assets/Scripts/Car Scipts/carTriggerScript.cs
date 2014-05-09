using UnityEngine;
using System.Collections;

public class carTriggerScript : MonoBehaviour {

	public GameObject carCamera;
	public GameObject player;
	public CarUserControl carControlScript;
	public dfSprite crosshair;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.Equals(player))
		{
			//Switch to car, disable player
			carCamera.SetActive(true);
			player.SetActive(false);
			carControlScript.enabled = true;
			crosshair.IsVisible = false;
		}
	}
}
