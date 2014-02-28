using UnityEngine;
using System.Collections;

public class FadeInStart : MonoBehaviour {

	public GameObject lights;

	void Start () {
		PersistantGlobalScript.FreezeWorldForMenu = true;
		lights.SetActive(false);
	}

	void Update () {
	
	}
}
