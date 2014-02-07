using UnityEngine;
using System.Collections;

public class MinigameObjectScript : MonoBehaviour {

	/* Moves camera to predefined position for viewing object.
	 * Old camera position is saved and returned to once game is aborted.
	 * Most other game interactions disabled during the minigame.
	 * 
	 *  */
	public Vector3 newCameraPosition; 
	public Vector3 offset = new Vector3(0, 0, -1); //offset from the object for where to place the camera
	public float maxDistance = 3.0f; //max distance the player can be from the object but can still interact with it
	public Transform player;

	public bool mouseover = false;
	public bool minigameActive = false;
	private Quaternion oldCameraDirection;
	private Vector3 oldCameraPosition;

	void Update() {
		if (Input.GetMouseButton(0) && minigameActive && !mouseover) {
			PersistantGlobalScript.interactionEnabled = true;
			PersistantGlobalScript.mouseLookEnabled = true;
			PersistantGlobalScript.movementEnabled = true; 
			Camera.main.transform.position = oldCameraPosition;
			Camera.main.transform.rotation = oldCameraDirection;
			minigameActive = false;
			//Camera.main.transform.LookAt (this.transform.position);
		}
	}

	void OnMouseDown() {
		float distance = Mathf.Abs((transform.position - player.position).magnitude);
		if (distance < maxDistance && !minigameActive) {
			PersistantGlobalScript.interactionEnabled = false;
			PersistantGlobalScript.mouseLookEnabled = false;
			PersistantGlobalScript.movementEnabled = false; 
			newCameraPosition = transform.position;
			newCameraPosition += offset;
			oldCameraPosition = Camera.main.transform.position;
			oldCameraDirection = Camera.main.transform.rotation;
			Camera.main.transform.position = newCameraPosition;
			Camera.main.transform.LookAt (this.transform.position);
			minigameActive = true;
		}
	}

	void OnMouseEnter() {
		mouseover = true;
	}
	
	void OnMouseExit() {
		mouseover = false;
	}
}
