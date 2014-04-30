using UnityEngine;
using System.Collections;

public class DFGUIAudioOnActive : MonoBehaviour {

	public dfLabel triggerLabel;
	public AudioClip soundEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (triggerLabel.IsVisible)
		{
			audio.PlayOneShot(soundEffect);
		}
	}
}
