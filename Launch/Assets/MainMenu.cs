using UnityEngine;
using System.Collections;

/// <summary>
/// Code to handle the main menu
/// </summary>
public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;
	Rect viewRect = new Rect(0,0, Screen.width, Screen.height);

	void OnGUI()
	{
		//Show background texture
		GUI.DrawTexture(viewRect, backgroundTexture, ScaleMode.ScaleAndCrop);

		//"Play" button
		if (GUI.Button(new Rect(128, Screen.height * 0.8f, Screen.width * 0.2f, Screen.height * 0.1f), "Begin Mission"))
		{

		}

		//"Play" button
		if (GUI.Button(new Rect(128, Screen.height * 0.8f, Screen.width * 0.2f, Screen.height * 0.1f), "Options"))
		{
			
		}
	}
}
