using UnityEngine;
using System.Collections;
/// <summary>
/// Always exists in any scene, handles global variables and methods
/// </summary>
public class PersistantGlobalScript : MonoBehaviour
{
	/// <summary>
	/// Allow mouse to control camera
	/// </summary>
	public static bool mouseLookEnabled = true;
	/// <summary>
	/// Allow clicking on objects
	/// </summary>
	public static bool interactionEnabled = true;
	/// <summary>
	/// How long is the player allowed to hold the mouse button on an object before assuming they are dragging it
	/// </summary>
	public static bool movementEnabled = true;

	public static bool minigameActive = false;
	public static bool minigameMouseover = false;
	
	public bool mActive = false;

	public static float dragThreshold = 0.3f;
	/// <summary>
	/// Turn camera when mouse reaches an edge
	/// </summary>
	public static bool edgeTurnEnabled = false;
	//How long the left mouse button has been held down
	private static float clickTime = 0.0f;

//	public bool allowMouseLook {get; set;}
//	public bool allowInteraction {get; set;}
//	public bool allowMovement {get; set;}

	private static bool freezeWorldForMenu;

	//If on, disables all interaction and stops time
	public static bool FreezeWorldForMenu
	{
		get{ return freezeWorldForMenu; }
		set
		{
			//mouseLookEnabled = !value;
			interactionEnabled = !value;
			movementEnabled = !value;

			if (value)
			{
				Time.timeScale = 0.0f;
			}
			else Time.timeScale = 1.0f;
		}
	}

	// Use this for initialization
	void Start ()
	{
		//Prevent the Global Script object from being deleted when you leave the main menu
		//Object.DontDestroyOnLoad(this.gameObject);
	}

	void Update ()
	{
		//print (Time.timeScale);

		if(Input.GetMouseButtonUp(0))
		{
			if (clickTime > dragThreshold)
			{
				interactionEnabled = false;
				//print ("Click timeout");
			}
			clickTime = 0.0f;
		}
		else if(Input.GetMouseButton(0))
		{
			interactionEnabled = true; //Disabled to make minigames work
			clickTime += Time.deltaTime;
			//print ("Clicktime: " + clickTime);
		}
		mActive = minigameActive; //Silliness to deal with DFGUI

		if(Input.GetButtonUp("Fire2") && !edgeTurnEnabled)
		{
			mouseLookEnabled = !mouseLookEnabled;
			
		}
	}

	public void MinigameEventHanlder() {
		minigameMouseover = false;
	}

	public void bar() {

	}
}
