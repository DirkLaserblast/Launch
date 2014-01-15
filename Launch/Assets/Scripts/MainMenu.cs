using UnityEngine;
using System.Collections;

/// <summary>
/// Code to handle the main menu
/// </summary>
public class MainMenu : MonoBehaviour {

	/// <summary>
	/// The background texture.
	/// </summary>
	public Texture backgroundTexture;

	/// <summary>
	/// The menu font.
	/// </summary>
	public Font menuFont;

	private Rect viewRect = new Rect(0,0, Screen.width, Screen.height);
	private Vector2 buttonSize = new Vector2(128, 64);

	private string currentMenu = "main";

	void OnGUI()
	{
		//GUIStyle myStyle = new GUIStyle();
		//myStyle.font = menuFont;
		if (currentMenu == "main")
		{
			//Show background texture
			//GUI.DrawTexture(viewRect, backgroundTexture, ScaleMode.ScaleAndCrop);
			
			//"Play" button
			if (GUI.Button(new Rect(128, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Launch"))
			{
				
			}
			
			//"Settings" button
			if (GUI.Button(new Rect(128 + buttonSize.x + 64, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Settings"))
			{
				currentMenu = "settings";
			}
			
			//"Quit" button
			if (GUI.Button(new Rect(Screen.width - buttonSize.x - 128, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Quit"))
			{
				print ("Quitting...");
				Application.Quit();
			}
		}
		else if (currentMenu == "settings")
		{
			//"Back" button
			if (GUI.Button(new Rect(128 + buttonSize.x + 64, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Back"))
			{
				currentMenu = "main";
			}
		}

	}
}
