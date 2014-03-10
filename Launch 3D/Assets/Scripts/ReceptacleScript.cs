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
	public bool enabled = true;

	private bool powered;

	bool isPowered ()
	{
		return powered;
	}

	void OnTriggerEnter (Collider other)
	{
		print ("Triggered");
		if (other.gameObject == receptacleItem && enabled)
		{
			receptacleItem.transform.position = hiddenReceptacleItem.transform.position;
			receptacleItem.SetActive(false);
			hiddenReceptacleItem.SetActive(true);
			indicator.GetComponent<MeshRenderer>().material.color = Color.green;
			powered = true;
		}
	}

	void OnMouseDown ()
	{
		print ("Eject");
		if (powered)
		{
			GetComponent<CapsuleCollider>().enabled = false;
			hiddenReceptacleItem.SetActive(false);
			receptacleItem.SetActive(true);
			powered=false;
			indicator.GetComponent<MeshRenderer>().material.color = Color.yellow;
			StartCoroutine("wait", 3.0f);
		}
	}

	IEnumerator wait (float time)
	{
		yield return new WaitForSeconds(time);
		GetComponent<CapsuleCollider>().enabled = true;
		indicator.GetComponent<MeshRenderer>().material.color = Color.red;
	}
}
