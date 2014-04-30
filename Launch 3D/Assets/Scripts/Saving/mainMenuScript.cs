using UnityEngine;
using System.Collections;

public class mainMenuScript : MonoBehaviour {

	public dfButton playContinueButton;

	private int currentLevel;

	void Start()
	{
		currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
		//Saved game exists
		if (currentLevel > 0)
		{
			playContinueButton.Text = "Continue";
		}
		else
		{
			playContinueButton.Text = "Play";
		}
	}

	public void toGame()
	{
		//Load the main game
		Application.LoadLevel(1);
	}

	public void deleteSave()
	{
		PlayerPrefs.DeleteAll();
	}

	public void quit()
	{
		Application.Quit();
	}
}
