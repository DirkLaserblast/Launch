using UnityEngine;
using System.Collections;

/// <summary>
/// Code to handle the main menu
/// </summary>
public class MainMenu : MonoBehaviour {

	//Size of standard button
	private Vector2 buttonSize = new Vector2(128, 64);
	//Which menu we're on
	private string currentMenu = "main";
	//Should VSync be on?
	private bool verticalSync;
	//Should we be fullscreen?
	private bool fullScreen;
	private int resolutionIndex;
	private int qualityIndex;
	private float volume;

	private GUIContent[] qualityButtons = new GUIContent[6];

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

		//If there are saved settings already, use them
		if (PlayerPrefs.HasKey("VerticalSync"))
		{
			verticalSync = (PlayerPrefs.GetInt("VerticalSync") > 0) ? true : false;
		}
		else
		{
			verticalSync = false;
		}

		if (PlayerPrefs.HasKey("FullScreen")) 
		{
			fullScreen = (PlayerPrefs.GetInt("FullScreen") > 0) ? true : false;
		}
		else
		{
			fullScreen = false;
		}

		if (PlayerPrefs.HasKey("ResolutionIndex"))
		{
			resolutionIndex = Screen.resolutions.Length - 1;
		}
		else
		{
			resolutionIndex = Screen.resolutions.Length - 1;
		}
		if (PlayerPrefs.HasKey("QualityIndex"))
		{
			qualityIndex = PlayerPrefs.GetInt("QualityIndex");
		}
		else
		{
			qualityIndex = QualitySettings.GetQualityLevel();
		}
		if (PlayerPrefs.HasKey("Volume"))
		{
			volume = PlayerPrefs.GetFloat("Volume");
		}
		else 
		{
			volume = AudioListener.volume;
		}

		qualityButtons[0] = new GUIContent("Fastest");
		qualityButtons[1] =new GUIContent("Fast");
		qualityButtons[2] =new GUIContent("Simple");
		qualityButtons[3] = new GUIContent("Good");
		qualityButtons[4] =new GUIContent("Beautiful");
		qualityButtons[5] =new GUIContent("Fantastic");
	}

	public void mainMenu()
	{
		//"Play" button
		if (GUI.Button(new Rect(128, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Launch"))
		{
			//Start / resume the game
			Application.LoadLevel("Casey's Testing");
		}
		
		//"Settings" button
		if (GUI.Button(new Rect(128 + buttonSize.x + 64, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Settings"))
		{
			//Change to settings menu
			currentMenu = "settings";
		}
		
//		//"Quit" button
//		if (GUI.Button(new Rect(Screen.width - buttonSize.x - 128, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Quit"))
//		{
//			//Quit the game
//			Application.Quit();
//		}
	}

	public void settingsMenu()
	{
		//Settings pane
		GUI.Box(new Rect(128, 64, Screen.width - 256, Screen.height * 0.85f), "Game Settings");
		
		//Vertical Sync
		verticalSync = GUI.Toggle(new Rect(192, 128, 128, 32), verticalSync, "Vertical Sync");
		
		//Fullscreen
		fullScreen = GUI.Toggle(new Rect(192, 160, 128, 32), fullScreen, "Fullscreen");
		
		//Screen Resolution selection with combo box
		//int resolutionIndex = comboBoxControl.GetSelectedItemIndex();
		resolutionIndex = comboBoxControl.List(new Rect(192, 192, 128, 32), resolutionComboBox[resolutionIndex].text, resolutionComboBox, listStyle);
		//resolutionIndex = comboBoxControl.GetSelectedItemIndex();

		//Overall quality selection
		GUI.Label(new Rect(192, 240, 128, 32), "Graphics Quality");
		qualityIndex = GUI.Toolbar(new Rect(192, 256, 500, 32), qualityIndex, qualityButtons);
		
		
		//Slider to control volume
		GUI.Label(new Rect(192, 304, 256, 32), "Volume");
		volume = GUI.HorizontalSlider(new Rect(192, 320, 256, 32), volume, 0f, 1f);
		
		//Button to apply all changes, also records them to PlayerPrefs
		if (GUI.Button(new Rect(128 + buttonSize.x + 64, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Apply"))
		{
			if (verticalSync) 
			{
				QualitySettings.vSyncCount = 1;
				PlayerPrefs.SetInt("VerticalSync", 1);
			}
			else 
			{
				QualitySettings.vSyncCount = 0;
				PlayerPrefs.SetInt("VerticalSync", 0);
			}
			
			QualitySettings.SetQualityLevel(qualityIndex);
			PlayerPrefs.SetInt("QualityIndex", qualityIndex);
			
			Screen.SetResolution(Screen.resolutions[resolutionIndex].width, Screen.resolutions[resolutionIndex].height, fullScreen);
			PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);

			AudioListener.volume = volume;
			PlayerPrefs.SetFloat("Volume", volume);

			//Save settings
			PlayerPrefs.Save();
		}
		
		//"Back" button
		if (GUI.Button(new Rect(160, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Back"))
		{
			currentMenu = "main";
		}
	}

	private void OnGUI()
	{
		//GUIStyle myStyle = new GUIStyle();
		//myStyle.font = menuFont;
		if (currentMenu == "main")
		{
			mainMenu();

		}
		else if (currentMenu == "settings")
		{
			settingsMenu();
		}

	}
}
