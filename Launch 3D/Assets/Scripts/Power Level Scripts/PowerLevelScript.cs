﻿using UnityEngine;
using System.Collections;

public class PowerLevelScript : MonoBehaviour {

	public float initialValue;
	public float currentValue;
	public float offsetHeat;
	public float offsetVent;
	public float offsetWater;
	public float offsetLight;
	public float offsetAnalysis;
	public float offsetComm;
	public float offsetDiagnostics;
	public dfSlider sliderScript;

	void Start() {
		sliderScript = transform.GetComponent<dfSlider> ();
		sliderScript.Disable ();
	}

	void Update () {
		currentValue = initialValue + offsetHeat + offsetVent + offsetWater + offsetLight + offsetAnalysis + offsetComm + offsetDiagnostics;
	}
}
