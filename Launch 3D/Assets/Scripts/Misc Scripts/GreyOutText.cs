using UnityEngine;
using System.Collections;

public class GreyOutText : MonoBehaviour {

	public dfLabel label;
	
	void Awake () {
		label.Color = new Color32 (144, 144, 147, 255);
	}
}
