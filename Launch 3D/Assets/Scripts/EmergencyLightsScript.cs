using UnityEngine;
using System.Collections;

public class EmergencyLightsScript : MonoBehaviour {
	public GameObject lights;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(){
		StartCoroutine (waittime ());
	}
	void startLights(){
		lights.SetActive (true);
	}
	void closeLights(){
		lights.SetActive (false);
	}
	IEnumerator waittime(){
		yield return new WaitForSeconds (2.5f);
		startLights ();
		yield return new WaitForSeconds (0.1f);
		closeLights ();
		yield return new WaitForSeconds (0.2f);
		startLights ();
		yield return new WaitForSeconds (0.3f);
		closeLights ();
		yield return new WaitForSeconds (0.4f);
		startLights ();
		yield break;
	}
}
