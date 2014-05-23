using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour {

	public string text;
	public string trail;
	public GameObject Complete;
	private string trailBase;
	private int timerMax = 35;
	private int timer = 35;
	private int count = 0;
	public int fullCount = 0;
	private dfLabel dfLabelScript;

	void Start () {
		dfLabelScript = transform.GetComponent<dfLabel> ();
		trailBase = trail;
	}

	void Update() {
		if (dfLabelScript.IsVisible) {
			if (fullCount == 4) {
				Complete.SetActive (true);
				transform.gameObject.SetActive (false);
			}
				timer--;
				if (timer <= 0) {
					timer = timerMax;
					count++;
					fullCount++;
					if (count == 3) {
						count = 0;
						trail = trailBase;
					} else {
							trail += trail;
					}
				dfLabelScript.Text = this.text + this.trail;
			}
		}
	}
}
