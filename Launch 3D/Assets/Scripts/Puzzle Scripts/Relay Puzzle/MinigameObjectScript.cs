using UnityEngine;
using System.Collections;

public class MinigameObjectScript : MonoBehaviour {

	/* Moves camera to predefined position for viewing object.
	 * Old camera position is saved and returned to once game is aborted.
	 * Most other game interactions disabled during the minigame.
	 * 
	 *  */
//	public Vector3 newCameraPosition; 
	public Vector3 offset = new Vector3(0, 0, -1); //offset from the object for where to place the camera
	public float maxDistance = 3.0f; //max distance the player can be from the object but can still interact with it
	public Transform player;

	private bool mouseover = false;
	private bool minigameActive = false;
	private Quaternion oldCameraDirection;
	private Vector3 oldCameraPosition;
	public GameObject mainCamera;
	public GameObject minigameCamera;
	private FirstPersonCharacter FPCscript;

	public GameObject reticle;

	public GameObject RoverPuzzleObject;
	private RoverPuzzle RoverPuzzleScript;

	void Start() {
		//print ("Starting up MinigameObj script.");
		PersistantGlobalScript.interactionEnabled = true;
		FPCscript = player.GetComponent<FirstPersonCharacter> ();
		RoverPuzzleScript = RoverPuzzleObject.GetComponent<RoverPuzzle> ();
	}

	void Update() {
		if (Input.GetMouseButton(0) && minigameActive && !mouseover) {
			PersistantGlobalScript.interactionEnabled = true;
			PersistantGlobalScript.mouseLookEnabled = true;
			PersistantGlobalScript.movementEnabled = true;
			//Camera.main.transform.position = oldCameraPosition;
			//Camera.main.transform.rotation = oldCameraDirection;

			SimpleMouseRotator[] mouseLookScripts = GetComponents<SimpleMouseRotator>();
			foreach (SimpleMouseRotator mouseLookScript in mouseLookScripts) {
				mouseLookScript.enabled = true;
			}

			print ("This should not be happening after change camera.");
	
			RoverPuzzleObject.collider.enabled=true;
			RoverPuzzleScript.enabled=false;
			mainCamera.SetActive(true);
			minigameCamera.SetActive(false);

	//		minigameActive = false;
			//Camera.main.transform.LookAt (this.transform.position);
		}
	}

	void OnMouseUp() {
		//print ("CLICKY");
		if (PersistantGlobalScript.interactionEnabled) {
			float distance = Mathf.Abs((transform.position - player.position).magnitude);
			if (distance < maxDistance && !minigameActive) {
				PersistantGlobalScript.interactionEnabled = false;
				PersistantGlobalScript.mouseLookEnabled = false;
				PersistantGlobalScript.movementEnabled = false; 

	//			SimpleMouseRotator[] mouseLookScripts = GetComponents<SimpleMouseRotator>();
	//			foreach (SimpleMouseRotator mouseLookScript in mouseLookScripts)
	//			{
	//				mouseLookScript.enabled = false;
	//			}

				reticle.SetActive(false);
				print ("Changing cameras.");
				mainCamera.SetActive(false);
				minigameCamera.SetActive(true);

				player.gameObject.SetActive(false);
				RoverPuzzleObject.collider.enabled=false;

	//			minigameCamera.transform.position = transform.position + offset;
	//			minigameCamera.transform.rotation = transform.rotation;

	//			newCameraPosition = transform.position;
	//			newCameraPosition += offset;
	//			oldCameraPosition = Camera.main.transform.position;
	//			oldCameraDirection = Camera.main.transform.rotation;
	//			Camera.main.transform.position = newCameraPosition;
	//			Camera.main.transform.LookAt (this.transform.position);
				Screen.showCursor = true;
				FPCscript.lockCursor = false;
				Screen.lockCursor = false;
				PersistantGlobalScript.minigameActive = true; 
				RoverPuzzleScript.enabled=true;
				//print("foo");
			}
		}
	}

	void OnMouseEnter() {
		mouseover = true;
	}
	
	void OnMouseExit() {
		mouseover = false;
	}
}
