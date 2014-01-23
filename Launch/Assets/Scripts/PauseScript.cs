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

	//Size of standard button
	private Vector2 buttonSize = new Vector2(128, 64);

	// Use this for initialization
	void Start()
	{
		paused = false;
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
			}
			else Time.timeScale = 1;
		}
	}

	// Update is called once per frame
	void OnGUI()
	{
		if (paused)
		{
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "PAUSED");
			GUILayout.BeginArea(new Rect (Screen.width/2 - 64, Screen.height/2, 128, 500));

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
		}
	}
}
