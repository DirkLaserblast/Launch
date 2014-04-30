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
	private float firstTimer = 24f;
	public AudioClip FourMonths;
	public GameObject lights;
	
	void triggerStartLog() {
		audio.PlayOneShot(FourMonths);
		lights.SetActive(false);
		//PersistantGlobalScript.FreezeWorldForMenu = true;
	}

	void triggerInfoBox()
	{
		lights.SetActive(true);
		PersistantGlobalScript.FreezeWorldForMenu = false;
		bigBoxTextContent.Text = textContent;
		bigBoxTextContent.IsVisible = true;
		bigBoxPanel.IsVisible = true;
		StartCoroutine("TurnOff", timer);
	}

	void triggerInfoBoxOff()
	{
		bigBoxTextContent.IsVisible = false;
		bigBoxPanel.IsVisible = false;
	}



	void OnTriggerEnter() {
		if (!triggered)
		{
			triggerStartLog();
			//print("foo");
			if (playSound)
			{
				audio.Play();
			}
			if (cancelAllSound)
			{
				PersistantGlobalScript.StopAllAudio();
			}
			StartCoroutine("TurnOn", firstTimer);
		}
	}

	IEnumerator TurnOff(float t) {
		yield return new WaitForSeconds(t);
		triggerInfoBoxOff();
	}

	IEnumerator TurnOn(float t) {
		yield return new WaitForSeconds(t);
		triggerInfoBox();
	}

}
