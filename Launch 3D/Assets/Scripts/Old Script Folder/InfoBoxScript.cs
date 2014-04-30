using UnityEngine;
using System.Collections;

/// <summary>
/// Script to open text boxes when interacting with an object (when clicked or via trigger)
/// If you want to open the box by clicking the object, the object needs a Collider2D
/// </summary>
public class InfoBoxScript : MonoBehaviour {
	
	public string title;
	public string content;
	public bool triggerOnClick = true;
	public bool recordable = true;
	public dfLabel textLabel;

//	private bool boxOpen;
//	private Vector2 position;
//	private GameObject globalScriptsObject;
//	private ItemLogScript itemLog;
//	private PersistantGlobalScript globalScript;
	private bool recordedToLog;
	private Rect infoRect;

	void Start()
	{
//		globalScriptsObject = GameObject.Find("Global Scripts");
//		itemLog = globalScriptsObject.GetComponent<ItemLogScript>();
//		globalScript = globalScriptsObject.GetComponent<PersistantGlobalScript>();
	}

	void triggerInfoBox()
	{
		triggerInfoBox(title, content);
	}

	void triggerInfoBox(string boxTitle, string boxContent)
	{
		title = boxTitle;
		content = boxContent;

		recordedToLog = false;
		foreach (string[] itemString in ItemLogScript.LogArray)
		{
			if (itemString[0] == title)
			{
				recordedToLog = true;
			}
		}
//		boxOpen = true;
//		position.x = Input.mousePosition.x;
//		position.y = Screen.height - Input.mousePosition.y;

		textLabel.Text = boxContent;
		textLabel.IsVisible = true;
	}

	void OnMouseUp()
	{
		if(triggerOnClick && PersistantGlobalScript.interactionEnabled) triggerInfoBox();
	}

	void InfoWindow(int ID)
	{
//		GUILayout.Label(content);
//		
//		GUILayout.BeginHorizontal();
//		//Show the log record button if object wasn't already logged
//		if (GUILayout.Button("Close", GUILayout.Width(64)))
//		{
//			boxOpen = false;
//			PersistantGlobalScript.mouseLookEnabled = true;
//		}
//		
//		if (!recordedToLog && recordable)
//		{
//			ItemLogScript.addItem(title, content);
//			recordedToLog = true;
//		}
//		GUILayout.EndHorizontal();



	}
	
//	void OnGUI()
//	{
//
//		if (boxOpen)
//		{
//			Event e = Event.current;
//			Time.timeScale = 0;
//
//			infoRect = GUILayout.Window(0, new Rect(position.x, position.y, 256, 64), InfoWindow, title, GUILayout.Width(256));
//			if (e.type == EventType.MouseDown && !infoRect.Contains(e.mousePosition))
//			{
//				boxOpen = false;
//				PersistantGlobalScript.mouseLookEnabled = true;
//			}
//
//		}
//		else
//		{
//			//Time.timeScale = 1;
//		}
//	}
}
