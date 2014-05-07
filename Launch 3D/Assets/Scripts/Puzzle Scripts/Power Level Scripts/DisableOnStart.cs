using UnityEngine;
using System.Collections;

public class DisableOnStart : MonoBehaviour {

	public GameObject obj;

	void Start () {
		obj.SetActive (false);
	}
}
