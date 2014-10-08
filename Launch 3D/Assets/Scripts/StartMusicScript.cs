using UnityEngine;
using System.Collections;

public class StartMusicScript : MonoBehaviour {

	public Collider collider;

	void OnTriggerEnter (Collider Other)
	{
		if (Other.tag == "Player")
		{
			audio.Play();
			collider.enabled = false;
		}
	}
}
