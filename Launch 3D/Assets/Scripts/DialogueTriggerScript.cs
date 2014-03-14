using UnityEngine;
using System.Collections;

public class DialogueTriggerScript : MonoBehaviour {

	public dfPanel window;
	public dfLabel title;
	public dfLabel name;
	public dfLabel content;

	public string[] titles;
	public string[] names;
	public string[] contents;

	public AudioClip radioSound;

	private int position = 1;
	public bool read = false;

	public float timer = 24f;
	private bool playing = false;
	public GameObject player;
	private Rigidbody playerRB;
	public AudioClip voiceOver;
	public AudioClip music;
	public bool lockPlayer;

	void Start() {
		playerRB = player.GetComponent<Rigidbody>();
	}

	void Update() {
		if (playing) {
			transform.position = player.transform.position;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && !read)
		{
			audio.PlayOneShot(voiceOver);
			if (music != null) Camera.main.audio.PlayOneShot(music);
			//PersistantGlobalScript.FreezeWorldForMenu = true;

			/*window.IsVisible = true;
			title.Text = titles[0];
			name.Text = names[0];
			content.Text = contents[0];*/
			//audio.PlayOneShot(radioSound);
			playing = true;
			if(lockPlayer) {
				PersistantGlobalScript.mouseLookEnabled = false;
				PersistantGlobalScript.movementEnabled = false;
				playerRB.velocity = Vector3.zero;
			}
			StartCoroutine("TurnOff", timer);
		}
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
			if(lockPlayer) {
				PersistantGlobalScript.mouseLookEnabled = true;
				PersistantGlobalScript.movementEnabled = true;
			}
			gameObject.SetActive(false);
		//}
	}

	IEnumerator TurnOff(float t) {
		yield return new WaitForSeconds(t);
		EndVoice();
	}
}
