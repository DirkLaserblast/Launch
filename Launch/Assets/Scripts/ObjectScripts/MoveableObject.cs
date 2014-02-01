using UnityEngine;
using System.Collections;

public class MoveableObject : MonoBehaviour {

	/* Object that will spawn arrows for movement.
	 * Movement of the object is based on the angle of the arrows.
	 * Any number of arrows can be spawned (for any number of directions),
	 * and the code show is simply an example script for an object that 
	 * can move in the 4 cardinal directions.
	 * 
	 * Note: Arrows are children of this object and thus inheret active/inactive properties, and the rotation.
	 *  */

	public Transform leftPrefab;
	public Transform rightPrefab;
	public Transform upPrefab;
	public Transform downPrefab;

	//anchors (empty game objects) that prevent the moveable object from moving past them
	public Transform leftAnchor; 
	public Transform rightAnchor;
	public Transform upAnchor;
	public Transform downAnchor;

	//references to the created arrow objects
	private Transform leftArrow;
	private Transform rightArrow;
	private Transform upArrow;
	private Transform downArrow;

	public Vector3 leftOffset = new Vector3(-1.5f, 0f, 0f); //needs to be updated to be based on the object
	public Vector3 rightOffset = new Vector3(1.5f, 0f, 0f);
	public Vector3 upOffset = new Vector3(0f, 1.5f, 0f);
	public Vector3 downOffset = new Vector3(0f, -1.5f, 0f);

	//Set based on how the object should be moved
	public bool canMoveLeft = true;
	public bool canMoveRight = true;
	public bool canMoveUp = true;
	public bool canMoveDown = true;

	//Timeout timer. Arrows will kill themselves if this reaches 0.
	public int timer;
	

	void Start () {
		leftArrow = null;
		rightArrow = null;
		upArrow = null;
		downArrow = null;
	}
	
	void Update () {
		timer--;
		//CheckAnchors ();
	}

	void OnMouseDown() {
		if(leftArrow == null && canMoveLeft) {
			leftArrow = (Transform) Instantiate(leftPrefab, transform.position + leftOffset, transform.rotation * Quaternion.Euler(0, 0, 180));
			leftArrow.transform.parent = this.transform;
		}
		if(rightArrow == null && canMoveRight) {
			rightArrow = (Transform) Instantiate(rightPrefab, transform.position + rightOffset, transform.rotation);
			rightArrow.transform.parent = this.transform;
		}
		if(upArrow == null && canMoveUp) {
			upArrow = (Transform) Instantiate(upPrefab, transform.position + upOffset, transform.rotation * Quaternion.Euler(0, 0, 90));
			upArrow.transform.parent = this.transform;
		}
		if(downArrow == null && canMoveDown) {
			downArrow = (Transform) Instantiate(downPrefab, transform.position + downOffset, transform.rotation * Quaternion.Euler(0, 0, 270));
			downArrow.transform.parent = this.transform;
		}
		transform.Rotate (Vector3.forward, 12f); //For testing/showing off rotation

		timer = 500;
	}



	//Deactivates an arrow if the object isn't allowed to move past that point in that direction.
	void CheckAnchors() {
		if(leftArrow != null && leftAnchor != null) {
			if(transform.position.x <= leftAnchor.transform.position.x) {
				leftArrow.transform.gameObject.SetActive(false);
			} else {
				leftArrow.transform.gameObject.SetActive(true);
			}
		}
		if(rightArrow != null && rightAnchor != null) {
			if(transform.position.x >= rightAnchor.transform.position.x) {
				rightArrow.transform.gameObject.SetActive(false);
			} else {
				rightArrow.transform.gameObject.SetActive(true);
			}
		}
		if(upArrow != null && upAnchor != null) {
			if(transform.position.y >= upAnchor.transform.position.y) {
				upArrow.transform.gameObject.SetActive(false);
			} else {
				upArrow.transform.gameObject.SetActive(true);
			}
		}
		if(downArrow != null && downAnchor != null) {
			if(transform.position.y <= downAnchor.transform.position.y) {
				downArrow.transform.gameObject.SetActive(false);
			} else {
				downArrow.transform.gameObject.SetActive(true);
			}
		}
	}


}
