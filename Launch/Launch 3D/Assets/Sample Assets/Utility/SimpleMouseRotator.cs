﻿using UnityEngine;

public class SimpleMouseRotator : MonoBehaviour {
	
	// A mouselook behaviour with constraints which operate relative to
	// this gameobject's initial rotation.
	
	// Only rotates around local X and Y.
	
	// Works in local coordinates, so if this object is parented
	// to another moving gameobject, its local constraints will
	// operate correctly
	// (Think: looking out the side window of a car, or a gun turret
	// on a moving spaceship with a limited angular range)
	
	// to have no constraints on an axis, set the rotationRange to 360 or greater.

	public Vector2 rotationRange = new Vector3(70,70); 
	public float rotationSpeed = 10;
	public float dampingTime = 0.2f;
	public bool autoZeroVerticalOnMobile = true;
	public bool autoZeroHorizontalOnMobile = false;
	public bool relative = true;
	Vector3 targetAngles;
	Vector3 followAngles;
	Vector3 followVelocity;
	Quaternion originalRotation;

	private float inputH = 0;
	private float inputV = 0;
	
	public float activationRangeX = 300;
	public float activationRangeY = 40;
	
	//Code for Wii-style aiming
	//From http://answers.unity3d.com/questions/425712/how-can-i-move-the-camera-when-the-mouse-reaches-t.html
	Vector2 MouseScreenEdge( Vector2 margin ) {
		//Margin is calculated in px from the edge of the screen
		
		Vector2 half = new Vector2(Screen.width/2, Screen.height/2);
		
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

	// Use this for initialization
	void Start () {
		originalRotation = transform.localRotation;
	}

	// Update is called once per frame
	void Update () {


		// we make initial calculations from the original local rotation
		transform.localRotation = originalRotation;

		// read input from mouse or mobile controls
		inputH = 0;
		inputV = 0;
		if (relative)
		{
			if (PersistantGlobalScript.mouseLookEnabled && Input.GetMouseButton(1))
			{
				inputH = CrossPlatformInput.GetAxis("Mouse X");
				inputV = CrossPlatformInput.GetAxis("Mouse Y");
			}
			else if (PersistantGlobalScript.edgeTurnEnabled)
			{
				Vector2 mouseEdge = MouseScreenEdge(new Vector2(activationRangeX, activationRangeY));

				//print((mouseEdge.x/activationRangeX) + ", " + (mouseEdge.y/activationRangeY));

				if (!(Mathf.Approximately(mouseEdge.x, 0f)) && !(Mathf.Approximately(mouseEdge.y, 0f)))
				{
					inputH = Mathf.Clamp(mouseEdge.x/activationRangeX, -1.0f, 1.0f);
					inputV = Mathf.Clamp(mouseEdge.y/activationRangeY, -0.2f, 0.3f);
				}
				else if(!(Mathf.Approximately(mouseEdge.x, 0f)))
				{
					inputH = Mathf.Clamp(mouseEdge.x/activationRangeX, -1.0f, 1.0f);
				}
				else if(!(Mathf.Approximately(mouseEdge.y, 0f)))
				{
					inputV = Mathf.Clamp(mouseEdge.y/activationRangeY, -0.2f, 0.3f);
				}
			}
			else
			{
				inputH = 0;
				inputV = 0;
			}

			// wrap values to avoid springing quickly the wrong way from positive to negative
			if (targetAngles.y > 180) { targetAngles.y -= 360; followAngles.y -= 360; }
			if (targetAngles.x > 180) { targetAngles.x -= 360; followAngles.x-= 360; }
			if (targetAngles.y < -180) { targetAngles.y += 360; followAngles.y += 360; }
			if (targetAngles.x < -180) { targetAngles.x += 360; followAngles.x += 360; }

			#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8
			// on mobile, sometimes we want input mapped directly to tilt value,
			// so it springs back automatically when the look input is released.
			if (autoZeroHorizontalOnMobile) {
				targetAngles.y = Mathf.Lerp (-rotationRange.y * 0.5f, rotationRange.y * 0.5f, inputH * .5f + .5f);
			} else {
				targetAngles.y += inputH * rotationSpeed;
			}
			if (autoZeroVerticalOnMobile) {
				targetAngles.x = Mathf.Lerp (-rotationRange.x * 0.5f, rotationRange.x * 0.5f, inputV * .5f + .5f);
			} else {
				targetAngles.x += inputV * rotationSpeed;
			}
			#else
			// with mouse input, we have direct control with no springback required.
			targetAngles.y += inputH * rotationSpeed;
			targetAngles.x += inputV * rotationSpeed;
			#endif

			// clamp values to allowed range
			targetAngles.y = Mathf.Clamp ( targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f );
			targetAngles.x = Mathf.Clamp ( targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f );

		} else {

			inputH = Input.mousePosition.x;
			inputV = Input.mousePosition.y;

			// set values to allowed range
			targetAngles.y = Mathf.Lerp ( -rotationRange.y * 0.5f, rotationRange.y * 0.5f, inputH/Screen.width );
			targetAngles.x = Mathf.Lerp ( -rotationRange.x * 0.5f, rotationRange.x * 0.5f, inputV/Screen.height );


		}

		// smoothly interpolate current values to target angles
		followAngles = Vector3.SmoothDamp( followAngles, targetAngles, ref followVelocity, dampingTime );

		// update the actual gameobject's rotation
		transform.localRotation = originalRotation * Quaternion.Euler( -followAngles.x, followAngles.y, 0 );
	

		
//		else if (PersistantGlobalScript.edgeTurnEnabled)
//		{
//			Vector2 mouseEdge = MouseScreenEdge(new Vector2(activationRangeX, activationRangeY));
//			//verticalTurn(mouseEdge);
//			turnCamera(mouseEdge);
//		}
//		else
//		{
//			transform.localRotation = transform.localRotation;
//		}
	}


}
