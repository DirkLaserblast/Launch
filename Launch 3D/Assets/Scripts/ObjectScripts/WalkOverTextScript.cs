using UnityEngine;
using System.Collections;

public class WalkOverTextScript : MonoBehaviour {

	public string textContent;
	public dfPanel bigBoxPanel;
	public dfLabel bigBoxTextContent;
	public bool playSound;
	public bool cancelAllSound;
	private bool triggered = false;
	private float timer = 6f;
	
	void triggerInfoBox()
	{
		bigBoxTextContent.Text = textContent;
		bigBoxTextContent.IsVisible = true;
		bigBoxPanel.IsVisible = true;
	}

	void triggerInfoBoxOff()
	{
		bigBoxTextContent.IsVisible = false;
		bigBoxPanel.IsVisible = false;
	}



	void OnTriggerEnter() {
		if (!triggered)
		{
			triggerInfoBox ();
			//print("foo");
			if (playSound)
			{
				audio.Play();
			}
			if (cancelAllSound)
			{
				PersistantGlobalScript.StopAllAudio();
			}
			StartCoroutine("TurnOff", timer);
		}
	}

	IEnumerator TurnOff(float t) {
		yield return new WaitForSeconds(t);
		triggerInfoBoxOff();
	}

}
