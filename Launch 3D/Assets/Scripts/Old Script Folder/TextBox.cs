using UnityEngine;
using System.Collections;

public class TextBox : MonoBehaviour {

	public string textContent;
	public dfPanel panel;
	public dfLabel label;
	private float timer = 12f;

	
	void triggerInfoBox()
	{
		label.Text = textContent;
		label.IsVisible = true;
		//bigBoxPanel.IsVisible = true;
	}
	
	void triggerInfoBoxOff()
	{
		label.IsVisible = false;
		//bigBoxPanel.IsVisible = false;
	}
	
	void OnMouseDown() {
		if (PersistantGlobalScript.interactionEnabled)
		{
			triggerInfoBox ();
			StartCoroutine("TurnOff", timer);
		}
	}
	
	IEnumerator TurnOff(float t) {
		yield return new WaitForSeconds(t);
		triggerInfoBoxOff();
	}
}
