using UnityEngine;
using System.Collections;

public class ActivateOnStart : MonoBehaviour {

	public GameObject GO;

	void Update () {
		GO.SetActive (true);
	}

}
