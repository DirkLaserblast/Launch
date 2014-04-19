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

	void Start () {
		//print ("foo");
		sprite.RelativePosition = new Vector3 (x, y, 0);
	}
	
	void Update () {

	}

	public void MoveTo(int x, int y) {
		this.x = x;
		this.y = y;
		sprite = transform.GetComponent<dfSprite> ();
		sprite.RelativePosition = new Vector3 (x, y, 0);
		moved = true;
	}
}
