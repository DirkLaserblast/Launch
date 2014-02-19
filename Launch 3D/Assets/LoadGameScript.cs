using UnityEngine;
using System.Collections;

public class LoadGameScript : MonoBehaviour {

	public GameObject player;

	// Load player and item positions
	void Start ()
	{
		string closestNode = PlayerPrefs.GetString("ClosestSaveNode");

		//print(closestNode);
		try
		{
			player.transform.position = GameObject.Find(closestNode).transform.position;
		}
		catch (System.NullReferenceException ex)
		{
	
		}


		Vector3 playerRotation = PlayerPrefsX.GetVector3("PlayerRotation");

		try
		{
//			player.transform.LookAt (playerPosition);
//			player.transform.rotation = Quaternion.Euler (new Vector3 (0, player.transform.rotation.eulerAngles.y, 0));

			print ("Rotating player to " + playerRotation);
			player.transform.eulerAngles = new Vector3 (0, playerRotation.y, 0);
			player.GetComponent<SimpleMouseRotator>().enabled = true;
		}
		catch (System.NullReferenceException ex)
		{
			//print ("No save data for playerRotation");
		}

		string[] logbookArray = PlayerPrefsX.GetStringArray("Logbook");

		try
		{
			ItemLogScript.LogArray.AddRange(logbookArray);
		}
		catch (System.NullReferenceException ex)
		{
			
		}

		string[] inventoryArray = PlayerPrefsX.GetStringArray("Inventory");
		//Disable items that are in the inventory
		try
		{
			foreach (string item in inventoryArray)
			{
				GameObject current = GameObject.Find(item);
				Inventory.inventoryObjects.Add(current);
				current.SetActive(false);
			}
		}
		catch (System.NullReferenceException ex) {

		}
	}

}
