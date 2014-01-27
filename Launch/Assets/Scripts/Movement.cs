using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	
	public Transform[] waypoints;
	public float waypointRadius = 0.01f;//cirlce around each node
	public float damping = 10.0f; // bouncing time
	public bool loop = false;
	public float speed = 2.0f;

	private Vector3 currentHeading,targetHeading;
	private int targetwaypoint; // decides where to move towards.
	private Transform xform;
	private bool useRigidbody;
	private Rigidbody2D rigid;
	
	private object[] obj;

	
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
		/*
		// i tagged them in unity. finds all the waypoints
		obj = GameObject.FindGameObjectsWithTag("hi");
		foreach (object o in obj)
		{
			GameObject g = (GameObject) o;
			Debug.Log(g.name);
		}
		*/
	}
	
	
	// calculates a new heading
	protected void FixedUpdate ()
	{
		// this moves the guy
		// target head is current location, current heading is heading location derp
		
		targetHeading = waypoints[targetwaypoint].position - xform.position;
		currentHeading = Vector3.Lerp(currentHeading,targetHeading,damping*Time.deltaTime);
		//Debug.Log ("tgt head: " + targetHeading);
		//Debug.Log ("curr tgt: " + currentHeading);
		
	}
	
	// moves us along current heading
	protected void Update()
	{
		//if mouse is pressed and held
		if (Input.GetMouseButton (0) && GUIUtility.hotControl == 0) 
		{
			/*
			//raycast taht shit
			Plane playerPlane = new Plane(Vector3.up, xform.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist = 0.0f;
			if(playerPlane.Raycast(ray, out hitdist))
			{
				Vector3 targetPoint = ray.GetPoint(hitdist);
				mousePos = Input.mousePosition;
				Debug.Log(mousePos);

			}*/
			// mouse position based on camera
			Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition );
			//Debug.Log(pz);
			int savePos = -1;

			if (useRigidbody)// havent really test lol
				rigid.velocity = currentHeading * speed;
			else // we only ever use this
				xform.position +=currentHeading * Time.deltaTime * speed;
			//Debug.Log(Input.mousePosition);
			float save = Mathf.Infinity;

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
			//if(waypoints[savePos] > waypoints[targetwaypoint])
			//targetwaypoint = savePos;
			if(Vector3.Distance(xform.position,waypoints[targetwaypoint].position)<=waypointRadius)
			if(savePos<targetwaypoint)
				targetwaypoint--;
			else if(savePos> targetwaypoint)
				targetwaypoint++;
			Debug.Log(targetwaypoint);

			
			if(Vector3.Distance(xform.position,waypoints[targetwaypoint].position)<=waypointRadius)
			{
				targetwaypoint++;
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