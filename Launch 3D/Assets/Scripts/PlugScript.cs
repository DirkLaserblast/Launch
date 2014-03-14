using UnityEngine;
using System.Collections;

public class PlugScript : MonoBehaviour {

	public bool started = false;
	private float duration = 0.8f;
	public GameObject otherPlug;
	public GameObject door;
	private DoorScript doorScript;
	private PlugScript otherPlugScript;
	public bool pluggedIn;

	void Start() {
		otherPlugScript = otherPlug.GetComponent<PlugScript> ();
		doorScript = door.GetComponent<DoorScript> ();
	}

	void Update() {
		if (started && duration > 0) {
			transform.Translate(Vector3.right * Time.deltaTime * 0.2f);
			duration -= Time.deltaTime;
			if(duration <= 0) {
				pluggedIn = true;
				if(otherPlugScript.pluggedIn) {
					doorScript.isLocked = false;
				}
			}
		}
	}


	void OnMouseDown() {
		started = true;
	}
}


//print ("moo");
//Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//Vector3 delta = Vector3.zero;
//delta.y = (pos.y - oldPos.y);
//oldPos = pos;
//transform.position += delta;