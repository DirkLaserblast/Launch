using UnityEngine;
using System.Collections;

public class PlugScript : MonoBehaviour {

	private Vector3 oldPos;

	void OnMouseDrag() {
		print ("moo");
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 delta = Vector3.zero;
		delta.y = (pos.y - oldPos.y);
		oldPos = pos;
		transform.parent.transform.position += delta;
	}
}
