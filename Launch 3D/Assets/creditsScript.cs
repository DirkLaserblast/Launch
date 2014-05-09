using UnityEngine;
using System.Collections;

public class creditsScript : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start ()
	{
		PlayerPrefs.DeleteAll();
		yield return new WaitForSeconds(10);
		//Wipe save
		Application.LoadLevel(0);
	}

}
