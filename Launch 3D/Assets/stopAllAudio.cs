using UnityEngine;
using System.Collections;

public class stopAllAudio : MonoBehaviour {

	public AudioClip sound;

	// Use this for initialization
	void Start () {
		PersistantGlobalScript.StopAllAudio();
		audio.PlayOneShot(sound);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
