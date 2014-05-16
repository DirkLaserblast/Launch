using UnityEngine;
using System.Collections;

public class UpdateMessage : MonoBehaviour {
		
	public OnOffSector LSroom;
	public dfLabel label;
	// Update is called once per frame
	void Update () {
		if(!LSroom.isOn) {
			label.IsVisible = true;
		} else {
			label.IsVisible = false;
		}
	}
}
