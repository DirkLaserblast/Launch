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
		while (true) {
						yield return new WaitForSeconds (1.5f);
						startLights ();
						yield return new WaitForSeconds (1.5f);
						closeLights ();
				}

	}
}
