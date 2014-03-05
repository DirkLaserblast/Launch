using UnityEngine;
using System.Collections;

public class ReceptacleScript : MonoBehaviour {

	/// <summary>
	/// The item that should be placed in the receptacle
	/// </summary>
	public GameObject receptacleItem;
	/// <summary>
	/// The hidden "locked in" item that is shown when the receptacle is active (has the item in it)
	/// </summary>
	public GameObject hiddenReceptacleItem;
	public GameObject door;
	public bool enabled = true;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == receptacleItem && enabled)
		{
			door.GetComponent<DoorScript>().isLocked = false;
			Destroy(receptacleItem);
			hiddenReceptacleItem.SetActive(true);
		}
	}
}
