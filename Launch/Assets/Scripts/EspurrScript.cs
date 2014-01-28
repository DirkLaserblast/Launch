using UnityEngine;
using System.Collections;

public class EspurrScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnBecameInvisible()
	{
		print(transform.position);
		if(transform.position.y < 0)
		{
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
	


	}
}
