using UnityEngine;
using System.Collections;

public class LoadGameScript : MonoBehaviour {

	public GameObject player;

	void unlockMouseLook ()
	{
		//yield return new WaitForSeconds(1.0f);
		PersistantGlobalScript.FreezeWorldForMenu = false;
		player.GetComponent<SimpleMouseRotator>().enabled = true;
	}

	// Load player and item positions
	void Start ()
	{
		string latestSaveNode = PlayerPrefs.GetString("ClosestSaveNode");

		StartCoroutine("unlockMouseLook");

		//print(closestNode);
		try
		{
			GameObject node = GameObject.Find(latestSaveNode);
			player.transform.position = node.transform.position;
			player.transform.rotation = node.transform.rotation;
		}
		catch (System.NullReferenceException ex)
		{
			print ("No save node");
		}
	

//		Vector3 playerRotation = PlayerPrefsX.GetVector3("PlayerRotation");
//
//		try
//		{
////			player.transform.LookAt (playerPosition);
////			player.transform.rotation = Quaternion.Euler (new Vector3 (0, player.transform.rotation.eulerAngles.y, 0));
//
//			//print ("Rotating player to " + playerRotation);
//			player.transform.eulerAngles = new Vector3 (0, playerRotation.y, 0);
//			player.GetComponent<SimpleMouseRotator>().enabled = true;
//		}
//		catch (System.NullReferenceException ex)
//		{
//			//print ("No save data for playerRotation");
//		}

//		string[] logbookArray = PlayerPrefsX.GetStringArray("Logbook");
//
//		try
//		{
//			ItemLogScript.LogArray.AddRange(logbookArray);
//		}
//		catch (System.NullReferenceException ex)
//		{
//			
//		}



	}

}
