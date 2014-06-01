using UnityEngine;
using System.Collections;

public class mainMenuScript : MonoBehaviour {

	public dfButton playContinueButton;

	private int currentLevel;

	void Start()
	{
		//Saved game exists
		if (PlayerPrefsX.GetBool("Saved", false))
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
		if (PlayerPrefsX.GetBool("Saved", false))
		{
			Application.LoadLevel("alphaBuildScene");
		}
		else Application.LoadLevel("StartScene");
	}

	public void deleteSave()
	{
		playContinueButton.Text = "Play";
		PlayerPrefs.DeleteAll();
	}

	public void quit()
	{
		Application.Quit();
	}
}
