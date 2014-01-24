using UnityEngine;
using System.Collections;

/// <summary>
/// Script to open text boxes when interacting with an object (when clicked or via trigger)
/// If you want to open the box by clicking the object, the object needs a Collider2D
/// </summary>
public class InfoBoxScript : MonoBehaviour {
	
	public string title;
	public string content;
	public bool triggerOnClick;

	private bool boxOpen;
	private Vector2 position;
	private GameObject globalScriptsObject;
	private ItemLogScript itemLog;
	private bool recordedToLog;

	void Start()
	{
		globalScriptsObject = GameObject.Find("Global Scripts");
		itemLog = globalScriptsObject.GetComponent<ItemLogScript>();
	}

	void triggerInfoBox()
	{
		triggerInfoBox(title, content);
	}

	void triggerInfoBox(string boxTitle, string boxContent)
	{
		recordedToLog = false;
		foreach (string[] itemString in itemLog.getLogArray())
		{
			if (itemString[0] == title)
			{
				recordedToLog = true;
			}
		}
		boxOpen = true;
		position.x = Input.mousePosition.x;
		position.y = Screen.height - Input.mousePosition.y;
	}

	void OnMouseDown()
	{
		triggerInfoBox();
	}

	void OnGUI()
	{
		if (boxOpen)
		{
			Time.timeScale = 0;

			GUILayout.BeginArea(new Rect(position.x, position.y, 256, 400));
			GUILayout.Box(title);
			GUILayout.TextArea(content);

			GUILayout.BeginHorizontal();
			//Show the log record button if object wasn't already logged
			if (GUILayout.Button("Close", GUILayout.Width(64))) boxOpen = false;

			if (!recordedToLog)
			{
				if (GUILayout.Button("Record to Logbook"))
				{
					itemLog.addItem(title, content);
					recordedToLog = true;
				}
			}
			GUILayout.EndHorizontal();

			GUILayout.EndArea();
		}
		else Time.timeScale = 1;
	}
}
