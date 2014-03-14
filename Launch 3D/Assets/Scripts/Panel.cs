﻿using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {
	public double timeLeft = 1f;
	public bool started = false;

	void Start () {

	}
	
	void Update () {
		if (started && timeLeft > 0) {
			timeLeft -= Time.deltaTime;
			transform.parent.transform.Rotate(0, 120*Time.deltaTime, 0);
		}
	}

	void OnMouseDown() {
		started = true;
	}
}