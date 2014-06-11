using UnityEngine;
using System.Collections;

public class DisableAntiClipScript : MonoBehaviour {

	public ProtectCameraFromWallClip clipScript;

	void OnTriggerEnter (Collider Other)
	{
		if (Other.tag == "Player")
		{
			clipScript.enabled = false;
		}
	}
}
