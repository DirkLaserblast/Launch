﻿using UnityEngine;
using System.Collections;

public class DialogueTriggerScript : MonoBehaviour {

//	public dfPanel window;
//	public dfLabel title;
//	public dfLabel name;
//	public dfLabel content;
//
//	public string[] titles;
//	public string[] names;
//	public string[] contents;

	public AudioClip radioSound;

	private int position = 1;
	public bool read = false;

	public float timer = 24f;
	private bool playing = false;
	public GameObject player;
	private Rigidbody playerRB;
	public bool ActivateOnStart;
	public AudioClip voiceOver;
	public AudioClip music;

	void Start() {

		//Load saved state
		//gameObject.SetActive(PlayerPrefsX.GetBool(gameObject.name, true));
		
		playerRB = player.GetComponent<Rigidbody>();
		if (ActivateOnStart) {
			playLog ();
		}
	}

	void Update() {
		if (playing) {
			//transform.position = camera.transform.position;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && !read && !playing)
		{
			playLog ();
		}
	}

	public void playLog() {
		audio.PlayOneShot(voiceOver);
		if (music != null) Camera.main.audio.PlayOneShot(music);
		//PersistantGlobalScript.FreezeWorldForMenu = true;
		
		/*window.IsVisible = true;
			title.Text = titles[0];
			name.Text = names[0];
			content.Text = contents[0];*/
		//audio.PlayOneShot(radioSound);
		playing = true;

		StartCoroutine("TurnOff", timer);
	}

	void EndVoice ()
	{
		/*if (position < contents.Length)
		{
			title.Text = titles[position];
			name.Text = names[position];
			content.Text = contents[position];
			position++;
			audio.PlayOneShot(radioSound);
		}
		else if (!read)
		{*/
			//window.IsVisible = false;
			//PersistantGlobalScript.FreezeWorldForMenu = false;
		read = true;

		PlayerPrefsX.SetBool(gameObject.name, false);
			gameObject.SetActive(false);
		//}
	}

	IEnumerator TurnOff(float t) {
		yield return new WaitForSeconds(t);
		EndVoice();
	}
}
