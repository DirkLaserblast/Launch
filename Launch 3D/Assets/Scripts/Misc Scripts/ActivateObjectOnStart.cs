using UnityEngine;
using System.Collections;

public class ActivateObjectOnStart : MonoBehaviour {

	public GameObject obj;

	void Start () {
		obj.SetActive(true);
		print ("yup");
	}

}
