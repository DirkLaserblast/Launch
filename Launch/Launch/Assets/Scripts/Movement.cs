using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	
	public Transform[] waypoints;
	public float waypointRadius = 3.0f;//cirlce around each node
	public float damping = 10.0f;
	public bool loop = false;
	public float speed = 2.0f;

	private Vector3 currentHeading,targetHeading;
	private int targetwaypoint; // decides where to move towards.
	private Transform xform;
	private bool useRigidbody;
	private Rigidbody2D rigid;
	bool mouseUsed;
	private object[] obj;
	Vector3 pz;


	
	// Use this for initialization
	protected void Start () // start reads once
	{
		
		xform = transform;
		currentHeading = xform.forward;
		// herp derp error checking time.
		if(waypoints.Length<=0)
		{
			Debug.Log("No waypoints on "+name);
			enabled = false;
		}
		targetwaypoint = 0;
		if(rigidbody!=null)
		{
			useRigidbody = true;
		}
		else
		{
			useRigidbody = false;
		}
	}
	
	
	// calculates a new heading
	protected void FixedUpdate ()
	{
		// this is the actual move portion of the game
		targetHeading = waypoints[targetwaypoint].position - xform.position;
		//Debug.Log (currentHeading + " T: "+targetHeading + " and targetwaypoint: " + waypoints [targetwaypoint].position);
		currentHeading = Vector3.Lerp(currentHeading,targetHeading,damping*Time.deltaTime);
		
	}
		
	protected void Update()
	{
		// if the mouse is pressed toggle boolean
		if (Input.GetMouseButtonUp (0)) 
		{
			mouseUsed = !mouseUsed;
			pz = Camera.main.ScreenToWorldPoint(Input.mousePosition );
		}
		if(mouseUsed)
		{
			int savePos = -1;

			if (useRigidbody)// havent really test lol
				rigid.velocity = currentHeading * speed;
			else // we only ever use this
				xform.position += currentHeading* Time.deltaTime * speed;

			float save = Mathf.Infinity;

			// finds the closest waypoint to mouse.
			foreach(Transform t in waypoints)
			{
				//Debug.Log(t.position);
				if(Vector3.Distance(pz,t.position) <= save)
				{
					save = Vector3.Distance(pz,t.position);

					savePos++;
					//Debug.Log(savePos);
				}
				
			}
			// find the next waypoint in list
			if(Vector3.Distance(xform.position,waypoints[targetwaypoint].position)<=waypointRadius)
			if(targetwaypoint == waypoints.Length && savePos == 0)
				targetwaypoint--;
			else if(savePos<targetwaypoint)
				targetwaypoint--;
			else if(savePos> targetwaypoint)
				targetwaypoint++;

			// see if the player is withing the radius of the waypoint
			if(Vector3.Distance(xform.position,waypoints[targetwaypoint].position)<=waypointRadius)
			{
				//targetwaypoint++;
				if(targetwaypoint>=waypoints.Length)
				{
					targetwaypoint = 0;
					if(!loop)
						enabled = false;
				}
			}
		}
	}
	
	
	// draws shit in unity pretty nifty 
	public void OnDrawGizmos() 
	{
		Gizmos.color = Color.red;
		if(waypoints==null)
			return;
		for(int i=0;i< waypoints.Length;i++)
		{
			Vector3 pos = waypoints[i].position;
			if(i>0)
			{
				Vector3 prev = waypoints[i-1].position;
				Gizmos.DrawLine(prev,pos);
			}
		}
	}
	
}