using UnityEngine;
using System.Collections;

public class DummySlotScript : MonoBehaviour {

	public bool on = false;
	public Vector3 originalPosition;

	void Start() {
		originalPosition = transform.position;
	}
	public void dummyMethod() {
		print ("stuff");
	}

	public void OnMouseUp() {
		transform.position = originalPosition;
	}
}
