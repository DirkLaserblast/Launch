using UnityEngine;
using System.Collections;

public class AmbientAudioLoad : MonoBehaviour {

	public GameObject ventillationAudio;
	public GameObject beepAudio;

	// Load status of both audio sets
	void Start ()
	{
		ventillationAudio.SetActive(PlayerPrefsX.GetBool("VentillationAudio", true));
		beepAudio.SetActive(PlayerPrefsX.GetBool("BeepSounds", true));
	}
	

}
