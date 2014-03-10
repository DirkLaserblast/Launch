using UnityEngine;
using System.Collections;

public class DiagnosticsMessage : MonoBehaviour {

	public GameObject DiagnosticSlider;
	public GameObject UnallocatedSlider;
	private dfSlider diagnosticScript;
	private dfSlider unallocatedScript;
	private dfLabel labelScript;
	
	void Start () {
		labelScript = transform.GetComponent<dfLabel> ();
		diagnosticScript = DiagnosticSlider.GetComponent<dfSlider> ();
		unallocatedScript = UnallocatedSlider.GetComponent<dfSlider> ();
	}
	
	void Update () {
		if (diagnosticScript.Value >= 200f && unallocatedScript.Value > 0) {
			labelScript.IsVisible = false;
		} else if(!labelScript.IsVisible) {
			labelScript.IsVisible = true;
		}
	}
}
