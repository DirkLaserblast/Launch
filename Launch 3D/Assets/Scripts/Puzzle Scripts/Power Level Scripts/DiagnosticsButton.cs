using UnityEngine;
using System.Collections;

public class DiagnosticsButton : MonoBehaviour {

	public GameObject DiagnosticSlider;
	public GameObject UnallocatedSlider;
	private dfSlider diagnosticScript;
	private dfSlider unallocatedScript;
	private dfButton buttonScript;

	void Start () {
		buttonScript = transform.GetComponent<dfButton> ();
		buttonScript.Disable ();
		diagnosticScript = DiagnosticSlider.GetComponent<dfSlider> ();
		unallocatedScript = UnallocatedSlider.GetComponent<dfSlider> ();
	}
	
	void Update () {
		if (diagnosticScript.Value >= 200f && unallocatedScript.Value > 0) {
			buttonScript.Enable ();
		} else if(buttonScript.IsEnabled) {
			buttonScript.Disable();
		}
	}
}
