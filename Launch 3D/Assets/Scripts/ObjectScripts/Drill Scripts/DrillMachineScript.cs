using UnityEngine;
using System.Collections;

public class DrillMachineScript : MonoBehaviour {
	
	/* Moves camera to predefined position for viewing object.
	 * Old camera position is saved and returned to once game is aborted.
	 * Most other game interactions disabled during the minigame.
	 *  */

	public Vector3 offset = new Vector3(0, 0, -1); //offset from the object for where to place the camera
	public float maxDistance = 3.0f; //max distance the player can be from the object but can still interact with it
	public Transform player;
	public GameObject mainCamera;
	public GameObject minigameCamera;
	public GameObject drill; 
	private DrillScript drillScript;
	
	public bool completed = false;

	void Start() {
		drillScript = drill.GetComponent<DrillScript>();
	}

	
	void Update() {
		CheckForCompletion();
		CheckMinigameEnd();
	}
	
	void OnMouseUp() {
		if (PersistantGlobalScript.interactionEnabled) {
			float distance = Mathf.Abs((transform.position - player.position).magnitude);
			if (distance < maxDistance && !PersistantGlobalScript.minigameActive) {
				StartMinigame();
			}
		}
	}

	private void StartMinigame() {
		PersistantGlobalScript.interactionEnabled = false;
		PersistantGlobalScript.mouseLookEnabled = false;
		PersistantGlobalScript.movementEnabled = false; 
		PersistantGlobalScript.minigameMouseover = true;
		
		SimpleMouseRotator[] mouseLookScripts = GetComponents<SimpleMouseRotator>();
		foreach (SimpleMouseRotator mouseLookScript in mouseLookScripts)
		{
			mouseLookScript.enabled = false;
		}
		
		mainCamera.SetActive(false);
		minigameCamera.SetActive(true);
		
		minigameCamera.transform.position = transform.position + offset;
		minigameCamera.transform.LookAt(transform.position + new Vector3(0, offset.y, 0));
		PersistantGlobalScript.minigameActive = true;
	}

	private void CheckMinigameEnd() {
		if ((PersistantGlobalScript.minigameActive && !PersistantGlobalScript.minigameMouseover) || (completed && PersistantGlobalScript.minigameActive)) {
			PersistantGlobalScript.interactionEnabled = true;
			PersistantGlobalScript.mouseLookEnabled = true;
			PersistantGlobalScript.movementEnabled = true;
			
			SimpleMouseRotator[] mouseLookScripts = GetComponents<SimpleMouseRotator>();
			foreach (SimpleMouseRotator mouseLookScript in mouseLookScripts)
			{
				mouseLookScript.enabled = true;
			}
			
			mainCamera.SetActive(true);
			minigameCamera.SetActive(false);
			
			PersistantGlobalScript.minigameActive = false;
		}
	}

	private void CheckForCompletion() {
		if(PersistantGlobalScript.minigameActive) {
			if(drill.activeSelf) {
				if(drillScript.drilled) {
					completed = true;
				}
			}
		}
	}
	
	
}
