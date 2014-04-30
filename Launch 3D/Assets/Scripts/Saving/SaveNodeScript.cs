using UnityEngine;
using System.Collections;

public class SaveNodeScript : MonoBehaviour {

	/// <summary>
	/// How close can the player approach before the node activates.
	/// </summary>
	public float activationSensitivity;
	public bool nodeActive;

	void OnTriggerEnter(Collider objectCollided)
	{
		if(objectCollided.tag == "Player")
		{
			PlayerPrefs.SetString ("LatestSaveNode", gameObject.name);
		}
	}
}
