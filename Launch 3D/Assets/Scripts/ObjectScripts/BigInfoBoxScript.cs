using UnityEngine;
using System.Collections;

/// <summary>
/// Script to open large centered windows that display a texture2D image
/// </summary>
public class BigInfoBoxScript : MonoBehaviour {

	public string title;
	public Texture content;
	public bool triggerOnClick = true;
	public bool hasText;
	//public Vector2 textPosition = new Vector2 (50, 50);
	//public Vector2 textBoxSize = new Vector2 (50, 50);
	public string textContent;
	public dfPanel bigBoxPanel;
	public dfLabel bigBoxLabel;
	public dfTextureSprite bigBoxImage;
	public dfLabel bigBoxTextContent;

//	private GameObject globalScriptsObject;
//	private PersistantGlobalScript globalScript;

//	private bool boxOpen;
	private Rect windowRect;

	void Start()
	{
//		globalScriptsObject = GameObject.Find("Global Scripts");
//		globalScript = globalScriptsObject.GetComponent<PersistantGlobalScript>();

		//float windowHeight = Screen.height - 128;
		//float windowWidth = windowHeight * ((float) content.width / (float) content.height);
		//Vector2 windowPosition = new Vector2((Screen.width - windowWidth)/2, (Screen.height - windowHeight)/2);

		//windowRect = new Rect(windowPosition.x, windowPosition.y, windowWidth, windowHeight);
	}

	void triggerInfoBox()
	{
		triggerInfoBox(title, content);
	}
	
	void triggerInfoBox(string boxTitle, Texture boxContent)
	{
		//title = boxTitle;
		content = boxContent;

//		boxOpen = true;
		PersistantGlobalScript.mouseLookEnabled = false;

		bigBoxTextContent.Text = textContent;
		bigBoxImage.Texture = content;
		float aspectRatio = content.height / content.width;
		bigBoxImage.Size = new Vector2 (bigBoxImage.Width * aspectRatio, bigBoxImage.Height);
		bigBoxTextContent.Size = new Vector2 (bigBoxImage.Width * 0.9f, bigBoxImage.Height * 0.9f);

		if (hasText)
		{
			bigBoxTextContent.IsVisible = true;
		}
		else bigBoxTextContent.IsVisible = false;

		bigBoxPanel.IsVisible = true;
	}

	void OnMouseUp()
	{
		if (triggerOnClick && PersistantGlobalScript.interactionEnabled) triggerInfoBox();
	}

	void BigWindow(int ID)
	{
//		if (content) GUI.DrawTexture(new Rect(windowRect.width/10, windowRect.height/10, windowRect.width - windowRect.width/5, windowRect.height - windowRect.height/5), content, ScaleMode.ScaleToFit);
//
//		if (hasText)
//		{
//			Vector2 boxSize = new Vector2 ((textBoxSize.x/100) * windowRect.width, (textBoxSize.y/100) * windowRect.height);
//			GUI.Label(new Rect((textPosition.x/100) * windowRect.width - boxSize.x/2,
//			                   (textPosition.y/100) * windowRect.height - boxSize.y/2, 
//			                   boxSize.x, 
//			                   boxSize.y), 
//			          textContent);
//		}
//
//		if (GUI.Button(new Rect(windowRect.width/2 - 32, windowRect.height - 40, 64, 32), "Close"))
//		{
//			boxOpen = false;
//			PersistantGlobalScript.mouseLookEnabled = true;
//		}


	}

//	void OnGUI()
//	{
//
//		if (boxOpen)
//		{
//			Event e = Event.current;
//			Time.timeScale = 0.0f;
//			windowRect = GUI.Window(0, windowRect, BigWindow, title);
//
//			if (e.type == EventType.MouseDown && !windowRect.Contains(e.mousePosition))
//			{
//				boxOpen = false;
//				PersistantGlobalScript.mouseLookEnabled = true;
//			}
//		}
		//else Time.timeScale = 1.0f;
//	}
	
}
