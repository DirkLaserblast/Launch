using UnityEngine;
using System.Collections;

public class MoveDownArrow : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		if(transform.parent.transform.GetComponent<MoveableObject>().timer <= 0) {
			Destroy(this.transform.gameObject);
		}
	}
	
	void OnMouseDown() {
		transform.parent.transform.GetComponent<MoveableObject>().timer = 200;
	}
	
	void OnMouseDrag() {
		Vector2 mouse = Input.mousePosition;
		Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (mouse.x, mouse.y, 15)) - transform.position;
		pos.x = 0;
		if(pos.y > 0) {
			pos.y = 0;
		}
		transform.parent.transform.position += pos;
	}
}
