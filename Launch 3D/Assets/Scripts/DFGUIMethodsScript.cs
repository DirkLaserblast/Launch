using UnityEngine;
using System.Collections;

public class DFGUIMethodsScript : MonoBehaviour {

	public GameObject player;

	public void stopTime()
	{
		PersistantGlobalScript.FreezeWorldForMenu = true;
	}

	public void startTime()
	{
		PersistantGlobalScript.FreezeWorldForMenu = false;
	}

	public void saveGame()
	{
		//Find nearest door node to player
		GameObject[] doorNodes = GameObject.FindGameObjectsWithTag("Door Node");

		//print (doorNodes.Length);

		GameObject closestNode = doorNodes[0];
		float closestDist = Vector3.Distance(closestNode.transform.position, player.transform.position);

		foreach (GameObject node in doorNodes)
		{
			//print (node.name + " is " + Vector3.Distance(node.transform.position, player.transform.position) + " from the player.");

			float nodeDist = Vector3.Distance(node.transform.position, player.transform.position);

			if (nodeDist < closestDist)
			{
				//print ("Saving " + node.name + " as closest node.");
				closestNode = node;
				closestDist = nodeDist;
			}
		}

		//print (closestNode.name);
		PlayerPrefs.SetString("ClosestDoorNode", closestNode.name);
		PlayerPrefsX.SetVector3("PlayerPosition", player.transform.position);

		PlayerPrefs.Save();
	}

	public void quitGame()
	{
		Application.Quit();
	}
}
