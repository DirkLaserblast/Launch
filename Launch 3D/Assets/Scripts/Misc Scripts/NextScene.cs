using UnityEngine;
using System.Collections;

public class NextScene : MonoBehaviour {
	public AudioSource audioSource;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying) {
			Application.LoadLevel("AlphaBuildScene");
		}
	}
}
