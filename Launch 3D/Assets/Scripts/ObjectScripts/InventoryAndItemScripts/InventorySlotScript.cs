using UnityEngine;
using System.Collections;

public class InventorySlotScript : MonoBehaviour {

	public bool on = false;
	public int slotNumber = 0;
	private Vector3 originalPosition;

	void Start() {
		originalPosition = transform.localPosition;
	}


	void Update() {
		on = Inventory.checkItem(slotNumber);
		//a little inefficient, will fix later on.
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
