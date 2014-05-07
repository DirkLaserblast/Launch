using UnityEngine;
using System.Collections;

public class CameraLookAt : MonoBehaviour {

	public Transform obj;

	void Start () {
		transform.LookAt(obj);
	}
}
