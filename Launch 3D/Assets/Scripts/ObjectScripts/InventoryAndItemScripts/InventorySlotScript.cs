using UnityEngine;
using System.Collections;

public class InventorySlotScript : MonoBehaviour {

	public bool on = false;
	public int slotNumber = 0;
	private Vector3 originalPosition;
	private dfSlicedSprite spriteScript;

	void Start() {
		originalPosition = transform.localPosition;
		spriteScript = gameObject.GetComponent<dfSlicedSprite>();
	}


	void Update() {
		on = Inventory.checkItem(slotNumber);
		//a little inefficient, will fix later on.
		if(on) {
			Item itemScript = Inventory.getItem(slotNumber).gameObject.GetComponent<Item>();
			string s = itemScript.spriteName;
			spriteScript.SpriteName = s;
		}
	}

	void OnMouseDown() {
		PersistantGlobalScript.mouseLookEnabled = false;
	}
	
	void OnMouseUp() {
		//I'll clean this up a bit later
		bool canReceive = false;
		Transform item = Inventory.getItem(slotNumber);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit, 100);
		ItemReceive receiveScript = hit.transform.gameObject.GetComponent<ItemReceive>();

		if(receiveScript != null) {
			if(receiveScript.receiveable == item.gameObject) {
				canReceive = true;
			}
		}

		if (!canReceive) {
			transform.localPosition = originalPosition;
		} else {
			transform.localPosition = originalPosition;
			Inventory.RemoveAt(slotNumber);
			receiveScript.received = true;
		}
	}
}
