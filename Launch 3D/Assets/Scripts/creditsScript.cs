using UnityEngine;
using System.Collections;

public class creditsScript : MonoBehaviour {

	public string nextScene;
	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.DeleteAll(); //Wipe save
	}

	void FixedUpdate()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Application.LoadLevel(nextScene);
		}
	}

}
