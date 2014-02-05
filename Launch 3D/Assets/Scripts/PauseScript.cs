using UnityEngine;
using System.Collections;

/// <summary>
/// Pause menu support.
/// Stops time and opens the pause menu.
/// </summary>
public class PauseScript : MonoBehaviour {

	//Which key should pause / unpause
	public KeyCode pauseKey;

	private bool paused;
	private Vector2 scrollPosition;

//	private GameObject globalScriptsObject;
//	private ItemLogScript itemLog;
//	private PersistantGlobalScript globalScript;

	private int i;

	//Size of standard button
//	private Vector2 buttonSize = new Vector2(128, 64);

	// Use this for initialization
	void Start()
	{
		paused = false;

		//Find the Global Scripts object
//		globalScriptsObject = GameObject.Find("Global Scripts");
//		itemLog = globalScriptsObject.GetComponent<ItemLogScript>();
//		globalScript = globalScriptsObject.GetComponent<PersistantGlobalScript>();
	}

	void pause()
	{
		paused = true;
	}

	void Update()
	{
		if (Input.GetKeyDown(pauseKey))
		{
			paused = !paused;
			if (paused)
			{
				Time.timeScale = 0;
				PersistantGlobalScript.mouseLookEnabled = false;
			}
			else
			{
				Time.timeScale = 1;
				PersistantGlobalScript.mouseLookEnabled = true;
			}
		}
	}

	// Update is called once per frame
	void OnGUI()
	{
		if (paused)
		{

			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), ""); //Darkened background

			GUI.Box(new Rect(0, 0, 256, Screen.height), ""); //Left sidebar

			GUILayout.BeginArea(new Rect (0, 0, 256, Screen.height));

			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label("...Paused...");
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			//"Unpause" button
			if (GUILayout.Button("Resume"))
			{
				paused = false;
			}

			//"Settings" button
			if (GUILayout.Button("Settings"))
			{
				//Change to settings menu
			}

			//"Quit" button
			if (GUILayout.Button("Quit"))
			{
				//Quit the game
				Application.Quit();
			}
			GUILayout.EndArea();

			GUI.Box(new Rect(Screen.width - 256, 0, 256, Screen.height), ""); //Right sidebar (logbook)

			GUILayout.BeginArea(new Rect(Screen.width - 256, 0, 256, Screen.height), "");
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label("Mission Logbook");
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(256), GUILayout.Height(Screen.height - 64));

			i = 0;
			foreach (string[] item in ItemLogScript.getLogArray())
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("[" + item[0] + "]");
				if (GUILayout.Button("Delete"))
				{
					ItemLogScript.deleteByIndex(i);
				}
				GUILayout.EndHorizontal();
				GUILayout.Label(item[1]);
				i++;
			}

			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}
	}
}
