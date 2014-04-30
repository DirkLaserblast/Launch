using UnityEngine;
using System.Collections;

public class OnOffSector : MonoBehaviour {

	public string OnSprite;
	public string OffSprite;
	public bool isOn;
	private dfSprite dfScript;
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
	}

	public void ToggleOff() {
		isOn = false;
		dfScript.SpriteName = OffSprite;
	}
}
