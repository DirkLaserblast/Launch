using UnityEngine;
using System.Collections;

public class DFGUIMethodsScript : MonoBehaviour {

	public GameObject player;

	public void stopTime()
	{
		print ("Stopping Time");
		PersistantGlobalScript.FreezeWorldForMenu = true;
	}

	public void startTime()
	{
		print ("Starting Time");
		PersistantGlobalScript.FreezeWorldForMenu = false;
	}

	public void saveGame()
	{
		//Find nearest door node to player
		GameObject[] saveNodes = GameObject.FindGameObjectsWithTag("Save Node");

		//print (saveNodes.Length);

		GameObject closestNode = saveNodes[0];
		float closestDist = Vector3.Distance(closestNode.transform.position, player.transform.position);

		foreach (GameObject node in saveNodes)
		{
			//print (node.name + " is " + Vector3.Distance(node.transform.position, player.transform.position) + " from the player.");

			float nodeDist = Vector3.Distance(node.transform.position, player.transform.position);
			SaveNodeScript saveScript = node.GetComponent<SaveNodeScript>();

			if (nodeDist < closestDist && saveScript.nodeActive)
			{
				//print ("Saving " + node.name + " as closest node.");
				closestNode = node;
				closestDist = nodeDist;
			}
		}

		//print (closestNode.name);
		PlayerPrefs.SetString("ClosestSaveNode", closestNode.name);
		PlayerPrefsX.SetVector3("PlayerPosition", player.transform.position);
		PlayerPrefsX.SetVector3("PlayerRotation", player.transform.eulerAngles);
		print ("Rotation saved: " + player.transform.eulerAngles.x + " " + player.transform.eulerAngles.y + " " + player.transform.eulerAngles.z);

		//Save logbook
		string[] itemLogStringArray = (string[])ItemLogScript.LogArray.ToArray(typeof(string));
		PlayerPrefsX.SetStringArray("Logbook", itemLogStringArray);

		//Save inventory
//		string[] invetoryStringArray = (string[])Inventory.inventoryObjects.ToArray(typeof(string));
//		PlayerPrefsX.SetStringArray("Inventory", invetoryStringArray);

		PlayerPrefs.Save();
	}

	public void quitGame()
	{
		PlayerPrefs.Save();
		Application.Quit();
	}

	public void quitNoSave()
	{
		Application.Quit();
	}
}
