using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public bool locked;
	public bool enterable;
	public string destinationScene; //Which scene to go to
	public string destinatonNode; //Which node the player should be placed on in the new scene

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseUp()
	{
		if (!locked && enterable)
		{
			PlayerPrefs.SetString("PlayerCurrentScene", destinationScene);
			PlayerPrefs.SetString("PlayerCurrentNode", destinatonNode);
			Application.LoadLevel(destinationScene);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	void unlock() {
		locked = false;
	}
}
