using UnityEngine;
using System.Collections;

public class DragObjectScript : MonoBehaviour
{
	public float maxDistance = 100.0f;
	
	public float spring = 50.0f;
	public float damper = 5.0f;
	public float drag = 10.0f;
	public float angularDrag = 5.0f;
	public float distance = 0.2f;
	public bool attachToCenterOfMass = false;

	private bool clickLocked = false;

	private SpringJoint springJoint;
	private GameObject draggedObject;
	/// <summary>
	/// How long to wait before a click is handled as a drag instead of opening the GUI
	/// </summary>
	public float interactTimeLimit = 0.1f;
	
	void Update()
	{

		if(!Input.GetMouseButtonDown(0))
		{
			return;
		}
		else if (clickLocked)
		{
			clickLocked = false;
			return;
		}

		if (!PersistantGlobalScript.dragEnabled) {
			return;
		}


		Camera mainCamera = FindCamera();

		RaycastHit hit;
		if (Screen.lockCursor)
		{
			if(!Physics.Raycast(mainCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2)), out hit, maxDistance))
				return;
		}
		else
		{
			if(!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, maxDistance))
				return;
		}

		if(!hit.rigidbody || hit.rigidbody.isKinematic)
			return;

		//We're dragging an object, lock on
		clickLocked = true;

		if(!springJoint)
		{
			GameObject go = new GameObject("Rigidbody dragger");
			Rigidbody body = go.AddComponent<Rigidbody>();
			body.isKinematic = true;
			springJoint = go.AddComponent<SpringJoint>();
		}
		
		springJoint.transform.position = hit.point;
		if(attachToCenterOfMass)
		{
			Vector3 anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
			anchor = springJoint.transform.InverseTransformPoint(anchor);
			springJoint.anchor = anchor;
		}
		else
		{
			springJoint.anchor = Vector3.zero;
		}
		
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = hit.rigidbody;

		draggedObject = hit.collider.gameObject;

		StartCoroutine(DragObject(hit.distance));
	}
	
	IEnumerator DragObject(float distance)
	{
		float oldDrag             = springJoint.connectedBody.drag;
		float oldAngularDrag     = springJoint.connectedBody.angularDrag;
		springJoint.connectedBody.drag             = this.drag;
		springJoint.connectedBody.angularDrag     = this.angularDrag;
		Camera cam = FindCamera();
		Ray ray;
		

		float elapsedTime = 0.0f;
		while(clickLocked)
		{
//			if (Screen.lockCursor)
//			{
				ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0f));
//			}
//			else
//			{
//				ray = cam.ScreenPointToRay(Input.mousePosition);
//			}
			springJoint.transform.position = ray.GetPoint(distance);
			draggedObject.rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

			//PersistantGlobalScript.mouseLookEnabled = false;

			if (elapsedTime < interactTimeLimit)
			{
				PersistantGlobalScript.interactionEnabled = true;
			}
			else
			{
//				Vector3 n = Camera.main.transform.position - draggedObject.transform.position;
//				draggedObject.transform.rotation = Quaternion.LookRotation(n) * Quaternion.Euler(0, 180, 0);

				Vector3 cameraPos = Camera.main.transform.position;
				cameraPos.y = draggedObject.transform.position.y;

				draggedObject.transform.LookAt(cameraPos);
				draggedObject.transform.rotation *= Quaternion.Euler(0, 180, 0);

//				Quaternion newRotation = Quaternion.LookRotation(n) * Quaternion.Euler(0, 90, 0);
//				draggedObject.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, newRotation, Time.deltaTime * 1.0f);

				PersistantGlobalScript.interactionEnabled = false;
				PersistantGlobalScript.edgeTurnEnabled = true;
			}

			elapsedTime += Time.deltaTime;

			yield return null;
		}

		draggedObject.rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;

		//PersistantGlobalScript.mouseLookEnabled = true;
		PersistantGlobalScript.interactionEnabled = true;
		PersistantGlobalScript.edgeTurnEnabled = false;

		if(springJoint.connectedBody)
		{
			springJoint.connectedBody.drag             = oldDrag;
			springJoint.connectedBody.angularDrag     = oldAngularDrag;
			springJoint.connectedBody                 = null;
		}
	}
	
	Camera FindCamera()
	{
		if (camera)
			return camera;
		else
			return Camera.main;
	}
}