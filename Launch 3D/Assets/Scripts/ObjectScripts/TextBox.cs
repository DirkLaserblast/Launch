using UnityEngine;
using System.Collections;

public class TextBox : MonoBehaviour {

	public string textContent;
	public dfPanel bigBoxPanel;
	public dfLabel bigBoxTextContent;
	private float timer = 12f;

	
	void triggerInfoBox()
	{
		bigBoxTextContent.Text = textContent;
		bigBoxTextContent.IsVisible = true;
		//bigBoxPanel.IsVisible = true;
	}
	
	void triggerInfoBoxOff()
	{
		bigBoxTextContent.IsVisible = false;
		//bigBoxPanel.IsVisible = false;
	}
	
	
	
	void OnMouseDown() {
		triggerInfoBox ();
		StartCoroutine("TurnOff", timer);
	}
	
	IEnumerator TurnOff(float t) {
		yield return new WaitForSeconds(t);
		triggerInfoBoxOff();
	}
}
