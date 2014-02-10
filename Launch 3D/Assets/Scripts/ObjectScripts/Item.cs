using UnityEngine;
using System.Collections;

public class Item : Inventory {
	public bool isMoveable = true;
	public bool isLight = true;
	public bool isPickable = true;
	public float weight = 100;
	public AudioSource audioComponent;
	public AudioClip pickUpSound;
	public AudioClip placeSound;
	public AudioClip useSound;
	public string description;
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

		if(PersistantGlobalScript.interactionEnabled && !invis && isPickable){
			if (Input.GetMouseButtonUp (1)) {// as in inventory
				audioComponent.clip = pickUpSound;
				gameObject.renderer.enabled = false;
				if(gameObject.rigidbody)
					gameObject.rigidbody.isKinematic = true;
				gameObject.transform.parent = null;
				GameObject go = GameObject.Find("Player");
				go.transform.parent = gameObject.transform;
				audioComponent.Play();
				inv.Add(gameObject);

			} else if (Input.GetMouseButtonDown (0) && gameObject.renderer.enabled) {// drag
				audioComponent.clip = useSound;
				print(audioComponent.audio.clip.name);
				audioComponent.Play();
				
			} else if (Input.GetMouseButtonUp (0) && gameObject.renderer.enabled) {
				audioComponent.clip = placeSound;
				print(audioComponent.audio.clip.name);
				audioComponent.Play();
			}
		}
	}
}
