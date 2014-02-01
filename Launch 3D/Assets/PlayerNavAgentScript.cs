using UnityEngine;
using System.Collections;

public class PlayerNavAgentScript : MonoBehaviour {

	private NavMeshAgent agent;
	void Start() {
		agent = GetComponent<NavMeshAgent>();
	}
	void Update() {
		RaycastHit hit;
		if (Input.GetMouseButtonUp(1)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
				agent.SetDestination(hit.point);
			
		}
		else if (Input.GetMouseButton(0))
		{
			agent.Stop();
		}
	}
}
