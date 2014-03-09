using UnityEngine;
using System.Collections;

public class PowerLabelScript : MonoBehaviour {

	public string text;
	public float value;
	public float maxValue;
	
	void Update () {
		int temp = (int)value;
		text = temp + " / " + maxValue;
	}
}
