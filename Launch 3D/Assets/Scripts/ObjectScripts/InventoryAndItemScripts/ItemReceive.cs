using UnityEngine;
using System.Collections;

public class ItemReceive : MonoBehaviour {

	/* This script is attached to any object that can 
	 * recieve an inventory item. Simply set the receiveable
	 * object to the corrosponding inventory item, and then use
	 * the flag mess with other scripts.
	 *  */


	public GameObject receiveable;
	public bool received = false;
}
