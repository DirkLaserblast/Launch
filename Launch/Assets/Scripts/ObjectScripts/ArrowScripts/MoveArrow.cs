using UnityEngine;
using System.Collections;

public class MoveArrow : MonoBehaviour {
	
	public int cameraZ = 15; //camera distance for ScreenToWorldPoint to work.
	
	void Start () {
		print ("hi");
	}
	
	void Update () {
		if(transform.parent.transform.GetComponent<MoveableObject>().timer <= 0) {
			Destroy(this.transform.gameObject);
		}
	}
	
	void OnMouseDown() {
		transform.parent.transform.GetComponent<MoveableObject>().timer = 500;
	}
	
	void OnMouseDrag() {
		transform.parent.transform.GetComponent<MoveableObject>().timer = 500;

		/*	Explanation: 
		 *  The clicked point is projected onto the line defined by the
		 * 	angle of the arrow. This result point is then checked to see if it
		 *  is in front of the center of the arrow object. Then, and only then, 
		 *  is the object moved forward by the different of the posistions of the
		 *  result point and arrow.
		 *  */

		Vector2 mouse = Input.mousePosition;
		Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (mouse.x, mouse.y, cameraZ)) - transform.position;
		Vector3 mousereal = Camera.main.ScreenToWorldPoint (new Vector3 (mouse.x, mouse.y, cameraZ));
		bool doDrag = false;
		float theta = transform.rotation.eulerAngles.z;
		float vlength = Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y) * Mathf.Cos(Mathf.Abs(Mathf.Atan2(pos.y, pos.x) - theta * Mathf.Deg2Rad));
		float xprime = vlength * Mathf.Cos(Mathf.Deg2Rad*theta);
		float yprime = vlength * Mathf.Sin(Mathf.Deg2Rad*theta);
		Vector3 newpos = new Vector3(xprime, yprime, 0);
		if (theta < 180) {
			if (transform.position.y < (-1 / Mathf.Tan (Mathf.Deg2Rad * theta) * transform.position.x + mousereal.y + mousereal.x / Mathf.Tan (Mathf.Deg2Rad * theta))) {
				doDrag = true;
			}
		} else {
			if (transform.position.y > (-1 / Mathf.Tan (Mathf.Deg2Rad * theta) * transform.position.x + mousereal.y + mousereal.x / Mathf.Tan (Mathf.Deg2Rad * theta))) {
				doDrag = true;
			}
		}
		if (doDrag) {
			transform.parent.transform.position += newpos;
			print( Quaternion.Angle (transform.rotation, Quaternion.identity));
		}
	}
}

