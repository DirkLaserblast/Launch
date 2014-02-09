﻿using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public bool isMoveable = false;
	public bool isLight = true;
	public float weight = 100;
	public AudioSource audioComponent;
	public AudioClip pickUpSound;
	public AudioClip placeSound;
	public AudioClip useSound;
	private bool invis = false;
	
	// Use this for initialization
	
	void Start () {
		if (gameObject.rigidbody) {
			gameObject.rigidbody.mass = weight;
			if (isMoveable == false) {
				gameObject.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			}
		}
		if (isLight == false) {
			gameObject.light.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseOver(){ // plays all the soudns
		if (gameObject.renderer.enabled == false)
			invis = true;
		if(PersistantGlobalScript.interactionEnabled && !invis){
			if (Input.GetMouseButtonUp (1)) {// as in inventory
				audioComponent.clip = pickUpSound;
				print(audioComponent.audio.clip.name);
				gameObject.renderer.enabled = false;
				
				audioComponent.Play();
			} else if (Input.GetMouseButtonDown (0)) {// drag
				audioComponent.clip = useSound;
				print(audioComponent.audio.clip.name);
				audioComponent.Play();
				
			} else if (Input.GetMouseButtonUp (0)) {
				audioComponent.clip = placeSound;
				print(audioComponent.audio.clip.name);
				audioComponent.Play();
			}
		}
	}
}
