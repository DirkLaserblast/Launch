using UnityEngine;
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
			Vector3 backward = transform.TransformDirection(Vector3.back);
			Vector3 right = Quaternion.AngleAxis(feelerAngle, Vector3.up) * forward;
			Vector3 left = Quaternion.AngleAxis(-feelerAngle, Vector3.up) * forward;

			RaycastHit hitLeft;
			RaycastHit hitRight;
			RaycastHit hitBack;

			//Check feelers
			bool leftFeeler = Physics.Raycast(transform.position, left, out hitLeft, objectAvoidRange) && hitLeft.collider.gameObject.name != "Door Jammer";
			bool rightFeeler = Physics.Raycast(transform.position, right, out hitRight, objectAvoidRange) && hitRight.collider.gameObject.name != "Door Jammer";
			bool backFeeler = Physics.Raycast(transform.position, backward, out hitBack, 4f) && hitBack.collider.gameObject.name != "Door Jammer";

			//Backed into something
//			if (backFeeler)
//			{
//				//print ("Back feeler");
//				carController.Move (0f, 1f);
//			}

//			if (rigidbody.velocity.magnitude < 0.1f)
//			{
//				carController.Move(0f, -1f);
//			}
			//If both side feelers are triggered, we're in a corner or wall, reverse and turn
			if (rightFeeler && leftFeeler)
			{
				//print ("Both feelers");
				carController.Move(-1f, -1f);
			}
			//If right feeler is triggered, turn left
			else if (rightFeeler)
			{
				//print ("Right feeler");
				carController.Move(-1f, -0.5f);
			}
			//If left feeler is triggered, turn right
			else if (leftFeeler)
			{
				//print ("Left feeler");
				carController.Move(1f, -0.5f);
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
