using UnityEngine;
using System.Collections;

public class SetVisible : MonoBehaviour {
	public dfSprite dfobj;

	public void setVisible() {
		print ("lkljslkdfj");
		if(dfobj.IsVisible) {
			dfobj.IsVisible = false;
		} else {
			dfobj.IsVisible = true;
		}
	}
}
