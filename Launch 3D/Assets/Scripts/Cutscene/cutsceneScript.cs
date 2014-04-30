using UnityEngine;
using System.Collections;

public class cutsceneScript : MonoBehaviour {

	public Camera mainCamera;
	public Camera cutsceneCamera;
	public GameObject car;

	void OnTriggerEnter(Collider other)
	{
		print ("Cutscene triggered");

		car.GetComponent<CarUserControl>().enabled = false;
		car.GetComponent<CarAIControl>().enabled = true;

		mainCamera.gameObject.SetActive(false);
		cutsceneCamera.gameObject.SetActive(true);
	}
}
