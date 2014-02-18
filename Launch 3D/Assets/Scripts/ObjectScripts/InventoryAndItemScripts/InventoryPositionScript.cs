using UnityEngine;
using System.Collections;

public class InventoryPositionScript : MonoBehaviour {

	public Vector3 startPos;

	void OnMouseDown() {
		startPos = transform.position;
	}
}
