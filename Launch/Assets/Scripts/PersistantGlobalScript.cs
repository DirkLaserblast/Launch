using UnityEngine;
using System.Collections;

public class PersistantGlobalScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		//Prevent the Global Script object from being deleted when you leave the main menu
		Object.DontDestroyOnLoad(this.gameObject);
	}
	

}
