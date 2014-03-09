using UnityEngine;
using System.Collections;

public class PowerDataScript : MonoBehaviour {

	public float currentValue;
	public float initialValue;
	public float offset;
	public float inverseOffset;

	void Update () {
		offset = currentValue - initialValue;
		inverseOffset = -offset;
	}
}
