using UnityEngine;
using System.Collections;

public class MinigameObject : MonoBehaviour {

	/* Just a holder object, doesn't do much other than check if 
	 * clicks were made outside of its bounds. If they were, the object
	 * deactivates itself.
	 * */

	public bool mouseover = false;

	void Start() {
		transform.gameObject.SetActive(false);
		mouseover = true;
	}
	

	void Update () {
		if (Input.GetMouseButton (0) && !mouseover) {
			transform.gameObject.SetActive(false);
			mouseover = true;
		}
	}


	//for checking if you're clicking on or not on the minigame.
	void OnMouseEnter() {
		mouseover = true;
	}

	void OnMouseExit() {
		mouseover = false;
	}
}
