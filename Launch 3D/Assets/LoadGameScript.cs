using UnityEngine;
using System.Collections;

public class LoadGameScript : MonoBehaviour {

	public GameObject player;

	// Load player and item positions
	void Start ()
	{
		string closestNode = PlayerPrefs.GetString("ClosestDoorNode");

		//print(closestNode);
		try
		{
			player.transform.position = GameObject.Find(closestNode).transform.position;
		}
		catch (System.NullReferenceException ex)
		{
	
		}


		Vector3 playerPosition = PlayerPrefsX.GetVector3("PlayerPosition");

		try
		{
			player.transform.LookAt (playerPosition);
			player.transform.rotation = Quaternion.Euler (new Vector3 (0, player.transform.rotation.eulerAngles.y, 0));
		}
		catch (System.NullReferenceException ex)
		{
			
		}

		string[] logbookArray = PlayerPrefsX.GetStringArray("Logbook");

		try
		{
			ItemLogScript.LogArray.AddRange(logbookArray);
		}
		catch (System.NullReferenceException ex)
		{
			
		}
	}

}
