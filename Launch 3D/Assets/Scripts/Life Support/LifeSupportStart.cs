﻿using UnityEngine;
using System.Collections;

public class LifeSupportStart : MonoBehaviour {
	
	public Vector3 offset = new Vector3(0, 0, -1); //offset from the object for where to place the camera
	public float maxDistance = 3.0f; //max distance the player can be from the object but can still interact with it
	public Transform player;
	public GameObject mainCamera;
	public GameObject minigameCamera;
	public GameObject reticle;
	public GameObject instructions;
	private FirstPersonCharacter FPCscript;
	
	void Start() {
		FPCscript = player.GetComponent<FirstPersonCharacter> ();
	}
	
	void Update() {
		//CheckForCompletion();
		//CheckMinigameEnd();
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
		reticle.SetActive (false);
		instructions.SetActive (true);
		PersistantGlobalScript.minigameActive = true;
		FPCscript.lockCursor = false;
		Screen.lockCursor = false;
	}
	
	public void EndMinigame() {
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
		reticle.SetActive (true);
		instructions.SetActive (false);
		PersistantGlobalScript.minigameActive = false;
		FPCscript.lockCursor = true;
		Screen.lockCursor = true;
	}
	
}

