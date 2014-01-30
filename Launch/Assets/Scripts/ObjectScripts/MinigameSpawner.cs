using UnityEngine;
using System.Collections;

public class MinigameSpawner : MonoBehaviour {
	
	public GameObject minigame;
	
	void Start () {
		//minigame.SetActive (false);
	}
	
	void Update () {
		
	}
	
	void OnMouseDown() {
		minigame.SetActive(true);
		Vector3 pos = Camera.main.ScreenToWorldPoint (Vector3.zero);
		pos.z = 0;
		minigame.transform.position = pos;
		print (pos);
	}
	
	/* Idea is that the minigame is an object with a bunch of children.
	 * The Object is really just the backdrop. Child objects are what the
	 * actual game consists of. The minigame is moved onto the camera when
	 * the minigame object is clicked on, and then deactivated the player
	 * clicks away or closes the screen.
	 * 
	 * Somehow all other objects in the scene need to have their click functions
	 * surpressed while the 'minigame' is up. Not sure how this should be accomplished. */
	
}
