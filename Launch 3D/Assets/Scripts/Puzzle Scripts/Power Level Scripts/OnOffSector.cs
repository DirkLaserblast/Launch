using UnityEngine;
using System.Collections;

public class OnOffSector : MonoBehaviour {

	public string OnSprite;
	public string OffSprite;
	public bool isOn;
	private dfSprite dfScript;
	public dfButton button;
	public PowerManager powerManager;

	void Start() {
		dfScript = GetComponent<dfSprite>();
	}

	public void Toggle() {
		if(isOn) {
			isOn = false;
			powerManager.CurrentPower++;
			dfScript.SpriteName = OffSprite;
		} else {
			if(powerManager.CurrentPower > 0) {
				isOn = true;
				powerManager.CurrentPower--;
				dfScript.SpriteName = OnSprite;
			}
		}
	}

	public void ToggleOn() {
		isOn = true;
		dfScript.SpriteName = OnSprite;
		button.NormalBackgroundColor = new Color32 (40, 255, 0, 255);
		button.HoverBackgroundColor = new Color32 (0, 176, 30, 255);
		button.FocusBackgroundColor = button.NormalBackgroundColor;
	}

	public void ToggleOff() {
		isOn = false;
		dfScript.SpriteName = OffSprite;
		button.NormalBackgroundColor = new Color32 (196, 0, 0, 255);
		button.HoverBackgroundColor = new Color32 (255, 0, 0, 255);
		button.FocusBackgroundColor = button.NormalBackgroundColor;
	}
}
