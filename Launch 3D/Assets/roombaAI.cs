﻿using UnityEngine;
using System.Collections;

public class roombaAI : MonoBehaviour {

	public float objectAvoidRange = 2; //How close should the roomba get before it avoids an object
	public float feelerAngle = 30; //Angle in degrees between the left and right feelers
	public float velocityLimit = 1f;

	private CarController carController;

	// Use this for initialization
	void Start () {
		carController = GetComponent<CarController>();
	}
	
	void Update ()
	{
		if (rigidbody.velocity.magnitude < velocityLimit)
		{
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			Vector3 right = Quaternion.AngleAxis(30, Vector3.up) * forward;
			Vector3 left = Quaternion.AngleAxis(-30, Vector3.up) * forward;

			RaycastHit hitLeft;
			RaycastHit hitRight;


			//Check feelers
			bool leftFeeler = Physics.Raycast(transform.position, left, out hitLeft, objectAvoidRange) && hitLeft.collider.gameObject.name != "Door Jammer";
			bool rightFeeler = Physics.Raycast(transform.position, right, out hitRight, objectAvoidRange) && hitRight.collider.gameObject.name != "Door Jammer";
			
			//If both side feelers are triggered, we're in a corner or wall, reverse and turn
			if (rightFeeler && leftFeeler)
			{
				carController.Move(-0.1f, -1f);
			}
			//If right feeler is triggered, turn left
			else if (rightFeeler)
			{
				carController.Move(-0.5f, 0.5f);
			}
			//If left feeler is triggered, turn right
			else if (leftFeeler)
			{
				carController.Move(0.5f, 0.5f);
			}
			//Otherwise drive straight
			else carController.Move(0f, 1f);
			
//			//Worse AI to help make puzzle possible, just reverse and turn on detection
//			if (rightFeeler || leftFeeler)
//			{
//				carController.Move(0f, -1f);
//			}
//			else carController.Move(0f, 1f);
		}
	}
}
