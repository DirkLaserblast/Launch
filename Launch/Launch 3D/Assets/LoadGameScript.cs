using UnityEngine;
using System.Collections;

public class LoadGameScript : MonoBehaviour {

	public GameObject player;

	// Load player and item positions
	void Start ()
	{
		string closestNode = PlayerPrefs.GetString("ClosestDoorNode");
		if (closestNode != null)
		{
			//print(closestNode);
			player.transform.position = GameObject.Find(closestNode).transform.position;
		}

		Vector3 playerPosition = PlayerPrefsX.GetVector3("PlayerPosition");
		if (playerPosition != null)
		{
			player.transform.LookAt(playerPosition);
			player.transform.rotation = Quaternion.Euler(new Vector3(0, player.transform.rotation.eulerAngles.y, 0));
		}
	}

}
