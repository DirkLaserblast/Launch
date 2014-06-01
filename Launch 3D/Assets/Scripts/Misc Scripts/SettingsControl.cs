using UnityEngine;
using System.Collections;

public class SettingsControl : MonoBehaviour {

	public dfDropdown AADropDown;
	public dfDropdown QualityDropDown;
	public dfSprite Vsync;
	public dfSlider Volume;


	void Awake () {
		QualityDropDown.SelectedIndex = QualitySettings.GetQualityLevel ();
		AADropDown.SelectedValue = "x" + QualitySettings.antiAliasing;
		if (QualitySettings.vSyncCount == 0) {
			Vsync.IsVisible = false;
		} else {
			Vsync.IsVisible = true;
		}
		Volume.Value = (int) (AudioListener.volume * 100);
	}

	public void UpdateAA() {
		QualitySettings.antiAliasing = (int) Mathf.Pow(2, AADropDown.SelectedIndex);
	}

	public void UpdateQuality() {
		QualitySettings.SetQualityLevel(QualityDropDown.SelectedIndex);
	}

	public void UpdateVsync() {
		if(Vsync.IsVisible) {
			QualitySettings.vSyncCount = 1;
		} else {
			QualitySettings.vSyncCount = 0;
		}
	}

	public void UpdateVolume() {
		AudioListener.volume = ((float)Volume.Value) / 100;
	}
}
