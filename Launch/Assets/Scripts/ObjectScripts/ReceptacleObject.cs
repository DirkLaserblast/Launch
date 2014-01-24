using UnityEngine;
using System.Collections;

public class ReceptacleObject : MonoBehaviour {
	
	public bool open = false;
	
	//Animation FSM needs to be set to change the sprite when open is flipped.
	
	void Update () {
		
	}
	
	void OnMouseDown() {
		if (open) {
			//grab item
		}
		open = true;
	}
	
}