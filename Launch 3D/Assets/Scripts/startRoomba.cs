using UnityEngine;
using System.Collections;

public class startRoomba : MonoBehaviour {

	public GameObject roomba;

	void OnTriggerEnter (Collider Other)
	{
		if (Other.tag == "Player")
		{
			roomba.SetActive(true);
		}
	}
}
