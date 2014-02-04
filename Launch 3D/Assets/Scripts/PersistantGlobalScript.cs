using UnityEngine;
using System.Collections;
/// <summary>
/// Always exists in any scene, handles global variables and methods
/// </summary>
public class PersistantGlobalScript : MonoBehaviour
{
	//Mouselook control variables
	public bool mouseLookEnabled = true;

	// Use this for initialization
	void Start ()
	{
		//Prevent the Global Script object from being deleted when you leave the main menu
		Object.DontDestroyOnLoad(this.gameObject);
	}

}
