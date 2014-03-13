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

	private float timer = 24f;
	public AudioClip OhShitConversation;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && !read)
		{
			audio.PlayOneShot(OhShitConversation);
			//PersistantGlobalScript.FreezeWorldForMenu = true;

			/*window.IsVisible = true;
			title.Text = titles[0];
			name.Text = names[0];
			content.Text = contents[0];*/
			//audio.PlayOneShot(radioSound);
		
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
			gameObject.SetActive(false);
		//}
	}

	IEnumerator TurnOff(float t) {
		yield return new WaitForSeconds(t);
		EndVoice();
	}
}
