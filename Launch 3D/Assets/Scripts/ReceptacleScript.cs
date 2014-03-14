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
	public GameObject indicator;
	public GameObject door;
	//public bool enabled = true;
	public AudioClip engaged;
	public AudioClip ejecting;

	private bool powered = false;

	public bool isPowered ()
	{
		//print (powered);
		return powered;
	}
	
	void OnTriggerEnter (Collider other)
	{
		//print ("Triggered");
		//Cell engaged
		if (other.gameObject == receptacleItem)
		{
			receptacleItem.transform.position = hiddenReceptacleItem.transform.position;
			receptacleItem.SetActive(false);
			hiddenReceptacleItem.SetActive(true);
			StartCoroutine("engage", 1.0f);
			audio.PlayOneShot(engaged);
		}
	}

	void OnMouseDown ()
	{
		//print ("Eject");
		//Cell ejected
//		if (powered)
//		{
		GetComponent<CapsuleCollider>().enabled = false;
		hiddenReceptacleItem.SetActive(false);
		receptacleItem.SetActive(true);
		powered=false;
		door.GetComponent<DoorScript>().isLocked = true;
		indicator.GetComponent<MeshRenderer>().material.color = Color.yellow;
		audio.PlayOneShot(ejecting);
		StartCoroutine("eject", 3.0f);
//		}
	}

	IEnumerator eject (float time)
	{
		yield return new WaitForSeconds(time);
		GetComponent<CapsuleCollider>().enabled = true;
		indicator.GetComponent<MeshRenderer>().material.color = Color.red;
	}

	IEnumerator engage (float time)
	{
		yield return new WaitForSeconds(time);
		powered = true;
		door.GetComponent<DoorScript>().isLocked = false;
		indicator.GetComponent<MeshRenderer>().material.color = Color.green;
	}
}
