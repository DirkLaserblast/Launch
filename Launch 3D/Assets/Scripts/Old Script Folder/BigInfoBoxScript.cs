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

	}

	void triggerInfoBox()
	{
		triggerInfoBox(title, content);
	}
	
	void triggerInfoBox(string boxTitle, Texture boxContent)
	{
		title = boxTitle;
		content = boxContent;

//		boxOpen = true;
//		PersistantGlobalScript.mouseLookEnabled = false;

		bigBoxLabel.Text = title;
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
		bigBoxPanel.IsInteractive = true;
	}

	void OnMouseUp()
	{
		if (triggerOnClick && PersistantGlobalScript.interactionEnabled) triggerInfoBox();
	}


	
}
