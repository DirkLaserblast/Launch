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

			//"Unpause" button
			if (GUI.Button(new Rect(128, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Resume"))
			{
				paused = false;
			}

			//"Settings" button
			if (GUI.Button(new Rect(128 + buttonSize.x + 64, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Settings"))
			{
				//Change to settings menu
			}

			//"Quit" button
			if (GUI.Button(new Rect(Screen.width - buttonSize.x - 128, Screen.height * 0.8f, buttonSize.x, buttonSize.y), "Quit"))
			{
				//Quit the game
				Application.Quit();
			}
		}
	}
}
