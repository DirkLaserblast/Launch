using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {
	
	public void playAudio() {
		if(!audio.isPlaying) {
			audio.Play(0);
		} else {
			audio.Stop();
		}
	}
}
