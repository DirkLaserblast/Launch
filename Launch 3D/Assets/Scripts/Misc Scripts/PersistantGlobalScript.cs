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
	public static bool pauseActive = false;
	public static bool minigameMouseover = false;

	public FreeLookCam thirdPerson;

	public static bool dragEnabled = true;

	public bool mActive = false;

	public dfSprite crosshair;

	private bool enabled;
	
	public static float dragThreshold = 0.3f;
	/// <summary>
	/// Turn camera when mouse reaches an edge
	/// </summary>

	public static bool edgeTurnEnabled = false;
	//How long the left mouse button has been held down

	public GameObject pauseMenu;
	private static GameObject player;
	private int count = 0;

	private static float clickTime = 0.0f;

//	public bool allowMouseLook {get; set;}
//	public bool allowInteraction {get; set;}
//	public bool allowMovement {get; set;}

	private static bool freezeWorldForMenu = false;
	

	//If on, disables all interaction and stops time
	public static bool FreezeWorldForMenu
	{
		get{ return freezeWorldForMenu; }
		set
		{
			if (value)
			{
				print ("Freezing world");
			}
			else print ("Unfreezing world");
			mouseLookEnabled = !value;
			interactionEnabled = !value;
			movementEnabled = !value;

			Screen.showCursor = value;
			Screen.lockCursor = !value;

			if (value)
			{
				Time.timeScale = 0.0f;
			}
			else Time.timeScale = 1.0f;
		}
	}

	public static void StopAllAudio()
	{
		AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
		foreach(AudioSource audioS in allAudioSources)
		{
			audioS.Stop();
		}
	}
	
	// Use this for initialization
	void Start ()
	{

		//Prevent the Global Script object from being deleted when you leave the main menu
		//Object.DontDestroyOnLoad(this.gameObject);
		Application.runInBackground = true;
		player = GameObject.FindWithTag("Player");
	}

	void Update ()
	{
		//print (Time.timeScale);

		//print ("Interaction: " + interactionEnabled);

//		if (pauseMenu.IsVisible)
//		{
//			PersistantGlobalScript.FreezeWorldForMenu = true;
//			crosshair.IsVisible = false;
//		}

		if (count < 5)
		{ //Moves the cursor to the center of the screen. Still not sure why it won't start there.
			Screen.lockCursor = false;
			Screen.lockCursor = true;
			count++;
			crosshair.IsVisible = true;
		}


		if (Input.GetKeyDown(KeyCode.Tab))
		{
			//print ("Pause Menu Open");

			if (pauseMenu.active)
			{
				closeMenu();
			}
			else 
			{
				openMenu();
			}
		}

//		if(Input.GetMouseButtonUp(0))
//		{
//			if (clickTime > dragThreshold)
//			{
//				interactionEnabled = false;
//				//print ("Click timeout");
//			}
//			clickTime = 0.0f;
//		}
		if(Input.GetMouseButton(0) && !minigameActive && !pauseActive) {
			Screen.lockCursor = false;
			Screen.lockCursor = true;
		}
		mActive = minigameActive; //Silliness to deal with DFGUI


	}

	public void closeMenu() {
		PersistantGlobalScript.pauseActive = false;
		thirdPerson.enabled = enabled;
		PersistantGlobalScript.FreezeWorldForMenu = false;
		crosshair.IsVisible = true;
		if (minigameActive) {
			Screen.lockCursor = true;
			Screen.showCursor = true;
			Screen.lockCursor = false;
		}
		pauseMenu.SetActive(false);
	}

	public void openMenu() {
		PersistantGlobalScript.pauseActive = true;
		enabled = thirdPerson.enabled;
		thirdPerson.enabled = false;
		PersistantGlobalScript.FreezeWorldForMenu = true;
		pauseMenu.SetActive (true);
		crosshair.IsVisible = false;
	}

	public void MinigameEventHanlder() {
		minigameMouseover = false;
	}
}
