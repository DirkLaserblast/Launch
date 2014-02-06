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
	public static float dragThreshold = 0.3f;
	/// <summary>
	/// Turn camera when mouse reaches an edge
	/// </summary>
	public static bool edgeTurnEnabled = false;
	//How long the left mouse button has been held down
	private static float clickTime = 0.0f;

	// Use this for initialization
	void Start ()
	{
		//Prevent the Global Script object from being deleted when you leave the main menu
		Object.DontDestroyOnLoad(this.gameObject);
	}

	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			interactionEnabled = true;
			clickTime += Time.deltaTime;
		}
		if(Input.GetMouseButtonUp(0))
		{
			if (clickTime > dragThreshold) interactionEnabled = false;
			clickTime = 0.0f;
		}
	}
}
