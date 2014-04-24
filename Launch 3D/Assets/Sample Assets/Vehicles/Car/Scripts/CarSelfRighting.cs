using UnityEngine;
using System.Collections;

public class CarSelfRighting : MonoBehaviour
{

	// Automatically put the car the right way up, if it has come to rest upside-down.

	public float suspensionDistance = 0.7f;
	public float smooth = 0.5f;

    [SerializeField] private float waitTime = 3f;       	// time to wait before self righting
    [SerializeField] private float velocityThreshold = 1f;  // the velocity below which the car is considered stationary for self-righting
	private float lastOkTime; // the last time that the car was in an OK state

    void FixedUpdate ()
    {
        // is the car is the right way up
        if(transform.up.y > 0f || rigidbody.velocity.magnitude > velocityThreshold)
        {
//			foreach (WheelCollider wheel in GameObject.FindObjectsOfType<WheelCollider>())
//			{
//				wheel.suspensionDistance = suspensionDistance;
//			}
			lastOkTime = Time.time;
        }
		else
		{
//			foreach (WheelCollider wheel in GameObject.FindObjectsOfType<WheelCollider>())
//			{
//				wheel.suspensionDistance = Mathf.Clamp(wheel.suspensionDistance - 0.1f, 0.0f, 1.0f);
//			}
		}

		if (Time.time > lastOkTime + waitTime)
		{
			RightCar ();
		}
    }

    // put the car back the right way up:
    void RightCar ()
    {
        // set the correct orientation for the car, and lift it off the ground a little
		transform.position = transform.position + Vector3.up * 2;
		transform.rotation = Quaternion.LookRotation(transform.up);
    }
}
