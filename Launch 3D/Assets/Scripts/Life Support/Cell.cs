using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	int x = 0;
	int y = 0;
	bool moved = false;
	dfSprite sprite;
	public dfSprite light;
	public dfSprite sink;
	public dfSprite fan;
	private bool hasLight;
	private bool hasFan;
	private bool hasSink; 

	public bool HasLight {
		get { return hasLight; }
		set {
			if(value && !hasLight) {
				light.IsVisible = true;
			}
			if(!value && hasLight) {
				light.IsVisible = false;
			}
			hasLight = value;
		}
	}

	public bool HasFan {
		get { return hasFan; }
		set {
			if(value && !hasFan) {
				fan.IsVisible = true;
			}
			if(!value && hasFan) {
				fan.IsVisible = false;
			}
			hasFan = value;
		}
	}
	public bool HasSink {
		get { return hasSink; }
		set {
			if(value && !hasSink) {
				sink.IsVisible = true;
			}
			if(!value && hasSink) {
				sink.IsVisible = false;
			}
			hasSink = value;
		}
	}

	void Start () {
		sprite = transform.GetComponent<dfSprite> ();
		if (Random.value > 0.9) {
			hasFan = true;
			fan.IsVisible = true;
		}
		sprite.RelativePosition = new Vector3 (x, y, 0);
		sprite.PerformLayout ();
	}


	public void MoveTo(int x, int y) {
		this.x = x;
		this.y = y;
		sprite = transform.GetComponent<dfSprite> ();
		sprite.RelativePosition = new Vector3 (x, y, 0);
		moved = true;
	}

}
