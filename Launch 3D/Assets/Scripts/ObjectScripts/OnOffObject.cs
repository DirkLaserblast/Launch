using UnityEngine;
using System.Collections;

public class OnOffObject : MonoBehaviour {
	public bool toggleOnOff = true;
	public AudioSource audioComponent;
	public Light lightComponent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if (toggleOnOff) {
//			Camera.main.backgroundColor = Color.grey;
//		}
//		else {
//			Camera.main.backgroundColor = Color.magenta;
//		}
	}

	void OnMouseUp ()
	{
		if (PersistantGlobalScript.interactionEnabled)
		{
			toggleOnOff = !toggleOnOff;
			if (audioComponent) audioComponent.mute = !audioComponent.mute;
			if (lightComponent) lightComponent.enabled = !lightComponent.enabled;
		}
	}
}
