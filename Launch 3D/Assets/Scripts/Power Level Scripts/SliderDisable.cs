using UnityEngine;
using System.Collections;

public class SliderDisable : MonoBehaviour {

	public GameObject dfObject;
	public bool overPower;
	private dfSlider dfSliderScript;
	private dfLabel dfLabelScript;

	void Start() {
		overPower = false;
		dfSliderScript = dfObject.GetComponent<dfSlider> ();
		dfLabelScript = transform.GetComponent<dfLabel> ();

	}

	void Update () {
		if (dfSliderScript.Value <= 0f) {
			overPower = true;
			dfLabelScript.Color = UnityEngine.Color.red;
		} else {
			overPower = false;
			dfLabelScript.Color = UnityEngine.Color.white;
		}
	}
}
