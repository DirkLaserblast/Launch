using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuSettingsScript : MonoBehaviour {
	
	public List<GameObject> UIelements = new List<GameObject>();
	public GameObject menu;

	public void showMenu() {
		menu.SetActive (true);
		foreach(GameObject ui in UIelements) {
			ui.SetActive(false);
		}
	}

	public void closeMenu() {
		menu.SetActive (false);
		foreach(GameObject ui in UIelements) {
			ui.SetActive(true);
		}
	}
}
