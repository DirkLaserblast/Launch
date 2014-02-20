using UnityEngine;
using System.Collections;

public class Item : Inventory {
	public string spriteName;
	public bool isMoveable = true;
	public bool isLight = true;
	public bool isPickable = true;
	public float weight = 100;
	public AudioSource audioComponent;
	public AudioClip pickUpSound;
	public AudioClip placeSound;
	public AudioClip useSound;
	public string description;
	public float pickUpDistance = 3.0f;
	
	
	// Use this for initialization
	private Transform thePlayer;
	private float dist = 9999999.9f;
	private bool canPickUp; // to see if the player is close enough to pick up
	
	//static Inventory playerInv;
	
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
	
	void RetrievePlayer (){// Inventory theInv  
		thePlayer = GameObject.FindGameObjectWithTag("Player").transform;
		//print (thePlayer.transform.position.x);
	}
	
	// Update is called once per frame
	void Update () {
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
		}
	}
	
	public void PickUpItem(){
		if (isPickable) {
			if(canPickUp){
				Inventory.addItem(gameObject.transform);
				gameObject.renderer.enabled = false;
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
		
		print ("Item Dropped: " + Inventory.inv.Count);
		Inventory.dropItem (gameObject.transform);
		Physics.IgnoreCollision(gameObject.collider,thePlayer.collider,false);
		
		
	}
	
}