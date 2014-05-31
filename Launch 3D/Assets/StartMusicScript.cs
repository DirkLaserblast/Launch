using UnityEngine;
using System.Collections;

public class StartMusicScript : MonoBehaviour {

	void OnTriggerEnter (Collider Other)
	{
		if (Other.tag == "Player")
		{
			audio.Play();
		}
	}
}
