using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	public float turnSensitivity = 5.0f;
	public float maxTurnSpeed = 20.0f;
	public int activationRangeX = 300;
	public int activationRangeY = 40;

//	private GameObject globalScriptsObject;
//	private PersistantGlobalScript globalScript;

	float camXRotation = 0F;

	//Code for Wii-style aiming
	//From http://answers.unity3d.com/questions/425712/how-can-i-move-the-camera-when-the-mouse-reaches-t.html
	Vector2 MouseScreenEdge( Vector2 margin ) {
		//Margin is calculated in px from the edge of the screen
		
		Vector2 half = new Vector2( Screen.width/2, Screen.height/2 );
		
		//If mouse is dead center, (x,y) would be (0,0)
		float x = Input.mousePosition.x - half.x;
		float y = Input.mousePosition.y - half.y;   
		
		//If x is not within the edge margin, then x is 0;
		//In another word, not close to the edge
		if( Mathf.Abs(x) > half.x - margin.x ) {
			x += (half.x - margin.x) * (( x < 0 )? 1 : -1);
		}
		else {
			x = 0f;
		}
		
		if( Mathf.Abs(y) > half.y - margin.y ) {
			y += (half.y - margin.y) * (( y < 0 )? 1 : -1);
		}
		else {
			y = 0f;
		}
		
		return new Vector2( x, y );
	}

	void turnCamera(Vector2 mouseEdge)
	{
		if (!(Mathf.Approximately(mouseEdge.x, 0f)) && !(Mathf.Approximately( mouseEdge.y, 0f)))
		{
			float camYRotation = transform.localEulerAngles.y + Mathf.Clamp(turnSensitivity*(mouseEdge.x/activationRangeX), -turnSensitivity, turnSensitivity);

			camXRotation += Mathf.Clamp(turnSensitivity*(mouseEdge.y/activationRangeY), -turnSensitivity, turnSensitivity);
			camXRotation = Mathf.Clamp (camXRotation, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-camXRotation, camYRotation, 0);
		}
		else if(!(Mathf.Approximately(mouseEdge.x, 0f)))
		{
			//Move your camera depending on the sign of mouse.Edge.x

			//float turnAmount = Mathf.Clamp(turnSensitivity*(mouseEdge.x/activationRangeX), -turnSensitivity, turnSensitivity);

			float camYRotation = transform.localEulerAngles.y + Mathf.Clamp(turnSensitivity*(mouseEdge.x/activationRangeX), -turnSensitivity, turnSensitivity);

			//transform.Rotate(0, turnAmount, 0);

			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, camYRotation, 0);
		}
		else if(!(Mathf.Approximately( mouseEdge.y, 0f)))
		{
			//Move your camera depending on the sign of mouse.Edge.y
			//print (transform.localEulerAngles.x + " " + transform.localEulerAngles.y);
			//			if(mouseEdge.y < 0 && transform.localEulerAngles.x > minimumY)
			//			{
			//				//Move Down
			//				transform.Rotate(new Vector3(-turnSensitivity*(mouseEdge.y/activationRangeY), 0, 0));
			//			}
			//			else if(transform.localEulerAngles.y < maximumY)
			//			{
			//				//Move Up
			//				transform.Rotate(new Vector3(-turnSensitivity*(mouseEdge.y/activationRangeY), 0, 0));
			//			}
			
			camXRotation += Mathf.Clamp(turnSensitivity*(mouseEdge.y/activationRangeY), -turnSensitivity, turnSensitivity);
			camXRotation = Mathf.Clamp (camXRotation, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-camXRotation, transform.localEulerAngles.y, 0);
		}
	}

//	void verticalTurn(Vector2 mouseEdge)
//	{
//		if(!(Mathf.Approximately( mouseEdge.y, 0f)))
//		{
//			//Move your camera depending on the sign of mouse.Edge.y
//			//print (transform.localEulerAngles.x + " " + transform.localEulerAngles.y);
//			if(mouseEdge.y < 0 && transform.localEulerAngles.x > minimumY)
//			{
//				//Move Down
//				transform.Rotate(new Vector3(-turnSensitivity*(mouseEdge.y/activationRangeY), 0, 0));
//			}
//			else if(transform.localEulerAngles.y < maximumY)
//			{
//				//Move Up
//				transform.Rotate(new Vector3(-turnSensitivity*(mouseEdge.y/activationRangeY), 0, 0));
//			}
//
//
//		}
//	}

	void Update ()
	{

		if (PersistantGlobalScript.mouseLookEnabled)
		{
			//Vector2 mouseEdge = MouseScreenEdge(new Vector2(activationRangeX, activationRangeY));
			
			if (axes == RotationAxes.MouseXAndY)
			{
				if (Input.GetMouseButton(0))
				{
					float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
					
					camXRotation += Input.GetAxis("Mouse Y") * sensitivityY;
					camXRotation = Mathf.Clamp (camXRotation, minimumY, maximumY);
					
					transform.localEulerAngles = new Vector3(-camXRotation, rotationX, 0);
				}
//				else
//				{
//					verticalTurn(mouseEdge);
//					horizontalTurn(mouseEdge);
//				}
				
			}
			else if (axes == RotationAxes.MouseX)
			{
				if (Input.GetMouseButton(0))
				{
					transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
				}
//				else horizontalTurn(mouseEdge);
				
			}
			else if (axes == RotationAxes.MouseY)
			{
				if (Input.GetMouseButton(0))
				{
					camXRotation += Input.GetAxis("Mouse Y") * sensitivityY;
					camXRotation = Mathf.Clamp (camXRotation, minimumY, maximumY);
					
					transform.localEulerAngles = new Vector3(-camXRotation, transform.localEulerAngles.y, 0);
				}
//				else verticalTurn(mouseEdge);
				
			}
		}
		else if (PersistantGlobalScript.edgeTurnEnabled)
		{
			Vector2 mouseEdge = MouseScreenEdge(new Vector2(activationRangeX, activationRangeY));
			//verticalTurn(mouseEdge);
			turnCamera(mouseEdge);
		}
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
//		globalScriptsObject = GameObject.Find("Global Scripts");
//		globalScript = globalScriptsObject.GetComponent<PersistantGlobalScript>();
	}
}