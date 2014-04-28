using UnityEngine;
using System.Collections;

public class tempCutsceneScript : MonoBehaviour {

	public AudioClip success;
	public GameObject secondTrigger;
	public bool quitOnTrigger = false;

	void OnTriggerEnter(Collider other)
	{
		audio.PlayOneShot(success);
		gameObject.GetComponentInChildren<SphereCollider>().enabled = false;

		if (secondTrigger) secondTrigger.SetActive(true);
		if (quitOnTrigger) Application.Quit();
	}
}
