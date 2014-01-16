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

	//private Rect viewRect = new Rect(0,0, Screen.width, Screen.height);

	//Size of standard button
	private Vector2 buttonSize = new Vector2(128, 64);
	//Which menu we're on
	private string currentMenu = "main";
	//Should VSync be on?
	private bool verticalSync;
	//Should we be fullscreen?
	private bool fullScreen;
	private int resolutionIndex;

	private GUIContent[] aspectButtons = new GUIContent[3];

	GUIContent[] resolutionComboBox;
	private ComboBox comboBoxControl = new ComboBox();
	private GUIStyle listStyle = new GUIStyle();

	private void Start()
	{
		int i = 0;
		resolutionComboBox = new GUIContent[Screen.resolutions.Length];

		foreach (Resolution res in Screen.resolutions)
		{
			resolutionComboBox[i] = new GUIContent(res.width + " x " + res.height);
			i++;
		}

//	    listStyle.normal.textColor = Color.white; 
//	    listStyle.onHover.background =
//	    listStyle.hover.background = new Texture2D(2, 2);
//	    listStyle.padding.left =
//	    listStyle.padding.right =
//	    listStyle.padding.top =
//	    listStyle.padding.bottom = 4;


		aspectButtons[0] = new GUIContent("4:3");
		aspectButtons[1] =new GUIContent("16:9");
		aspectButtons[2] =new GUIContent("16:10");
	}

	private void OnGUI()
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
				//Start / resume the game
			}
			
			//"Settings" button
			if (GUI.Button(new Rect(128 + buttonSize.x + 64, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Settings"))
			{
				//Change to settings menu
				currentMenu = "settings";
			}
			
			//"Quit" button
			if (GUI.Button(new Rect(Screen.width - buttonSize.x - 128, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Quit"))
			{
				//Quit the game
				Application.Quit();
			}
		}
		else if (currentMenu == "settings")
		{
			//Settings pane
			GUI.Box(new Rect(128, 64, Screen.width - 256, Screen.height * 0.85f), "Game Settings");

			//Vertical Sync
			verticalSync = GUI.Toggle(new Rect(192, 128, 128, 32), verticalSync, "Vertical Sync");

			//Fullscreen
			fullScreen = GUI.Toggle(new Rect(192, 160, 128, 32), fullScreen, "Fullscreen");

			//Aspect Ratio selection (a toolbar with three radio buttons)
			//GUI.Toolbar(new Rect(192, 192, 160, 32), aspectRatios, aspectButtons);

			//Screen Resolution selection with combo box
			int resolutionIndex = comboBoxControl.GetSelectedItemIndex();
			resolutionIndex = comboBoxControl.List(new Rect(192, 192, 128, 32), resolutionComboBox[resolutionIndex].text, resolutionComboBox, listStyle);
			//GUI.Label( new Rect(192, 192, 400, 21), 
			          //"You picked " + resolutionComboBox[resolutionIndex].text + "!" );

			//Button to apply all resolution changes
			if (GUI.Button(new Rect(192, 224, 128, 32), "Apply"))
			{
				Screen.SetResolution(Screen.resolutions[resolutionIndex].width, Screen.resolutions[resolutionIndex].height, fullScreen);
			}

			//GUI.HorizontalSlider(new Rect(192, 224, 160, 32), horizontalResolution, 640, Screen.);

			//"Back" button
			if (GUI.Button(new Rect(128 + buttonSize.x + 64, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Back"))
			{
				currentMenu = "main";
			}
		}

	}
}
