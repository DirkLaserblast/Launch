using UnityEngine;
using System.Collections;

public class PuzzleComplete : MonoBehaviour {

	public bool complete = false;
	public GameObject obj;
	
	// Update is called once per frame
	void Awake () {
		complete = true;
		obj.SetActive (true);
	}
}
