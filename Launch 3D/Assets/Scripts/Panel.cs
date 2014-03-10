using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {
	public double timeLeft = 3f;
	public bool started = false;

	void Start () {

	}
	
	void Update () {
		if (started && timeLeft > 0) {
			timeLeft -= Time.deltaTime;
			transform.Rotate(0, 110*Time.deltaTime, 0);
		}
	}

	void OnMouseDown() {
		started = true;
	}
}
