using UnityEngine;
using System.Collections;

public class WaitScript : MonoBehaviour {

	public float timer;

	void Start () {
	
	}

	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Application.LoadLevel("WinterQuarter");
		}
	}
}
