using UnityEngine;
using System.Collections;

public class roombaTriggerScript : MonoBehaviour {

	public Animator doorAnimator;

	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Roomba")
		{
			doorAnimator.SetBool("Jammed", false);
			doorAnimator.SetBool("Locked", false);
		}
	}
}
