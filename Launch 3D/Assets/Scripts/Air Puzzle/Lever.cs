using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {
	public double timeLeft = 1f;
	public bool started = false;

	void Start () {
	
	}
	
	void Update () {
		if (started && timeLeft > 0) {
			timeLeft -= Time.deltaTime;
			transform.Rotate(0, 0, 120*Time.deltaTime);
		}
	}
	
	void OnMouseDown() {
		started = true;
	}
}
