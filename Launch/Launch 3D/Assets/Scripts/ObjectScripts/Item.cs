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
<<<<<<< HEAD
<<<<<<< HEAD
	public string description;
	public float pickUpDistance = 3.0f;

=======
	private bool invis = false;
	
>>>>>>> ca293d238e912b2d3a5b2959ad288fafdfb5b47d
=======
	private bool invis = false;
	
>>>>>>> ca293d238e912b2d3a5b2959ad288fafdfb5b47d
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
<<<<<<< HEAD
<<<<<<< HEAD
		RetrievePlayer ();
		dist = Vector3.Distance(thePlayer.position, gameObject.transform.position);

		if (dist <= pickUpDistance)
		{
			canPickUp = true;
		}
		else
		{
			canPickUp = false;
			//theItem.PickUpItem();
		}

	}


	void OnMouseOver(){
		if(Input.GetMouseButtonUp(1) && canPickUp && gameObject.renderer.enabled){
			PickUpItem();
			//print ("Mouse UP");
=======
>>>>>>> ca293d238e912b2d3a5b2959ad288fafdfb5b47d
=======
>>>>>>> ca293d238e912b2d3a5b2959ad288fafdfb5b47d
		
	}
	
	void OnMouseOver(){ // plays all the soudns
		if (gameObject.renderer.enabled == false)
			invis = true;
		if(PersistantGlobalScript.interactionEnabled && !invis){
			if (Input.GetMouseButtonUp (1)) {// as in inventory
				audioComponent.clip = pickUpSound;
				print(audioComponent.audio.clip.name);
				gameObject.renderer.enabled = false;
<<<<<<< HEAD
<<<<<<< HEAD
				if (gameObject.rigidbody)
					gameObject.rigidbody.isKinematic = true;
				gameObject.light.enabled = true;
				MoveToPlayer(thePlayer.transform);
			}		
		}		
		
	}
	public void MoveToPlayer(Transform itemHolderObject){// parent to player
		canPickUp = false;
		Physics.IgnoreCollision(gameObject.collider,thePlayer.collider,true);
		//gameObject.SetActive(false);// when deactivate the whole gameobject will not do anything
		transform.parent = itemHolderObject;
		transform.localPosition = Vector3.zero;

	}

	public void DropItem(){// drop the item
		canPickUp = true;
		gameObject.light.enabled = true;
		//gameObject.SetActive(true);
		gameObject.renderer.enabled = true;
		if (gameObject.rigidbody)
			gameObject.rigidbody.isKinematic = false;
		gameObject.transform.parent = null;

		//print ("Item Dropped: " + Inventory.inv.Count);
		Inventory.dropItem (gameObject.transform);
		Physics.IgnoreCollision(gameObject.collider,thePlayer.collider,false);

		
=======
=======
>>>>>>> ca293d238e912b2d3a5b2959ad288fafdfb5b47d
				
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
<<<<<<< HEAD
>>>>>>> ca293d238e912b2d3a5b2959ad288fafdfb5b47d
=======
>>>>>>> ca293d238e912b2d3a5b2959ad288fafdfb5b47d
	}
}
