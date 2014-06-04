using UnityEngine;
using System.Collections;

public class roombaTriggerScript : MonoBehaviour {

	public Animator doorAnimator;
	public Animator blockerAnimator;

	private bool triggered = false;

	void Start()
	{
		//Load saved status of Roomba Puzzle
		doorAnimator.SetBool("Jammed", !PlayerPrefsX.GetBool("RoombaWin", false));
		doorAnimator.SetBool("Locked", !PlayerPrefsX.GetBool("RoombaWin", false));
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Roomba" && !triggered)
		{
			triggered = true;
			audio.Play();
			doorAnimator.SetBool("Jammed", false);
			doorAnimator.SetBool("Locked", false);
			//Fling the jamming object away
			blockerAnimator.SetBool("Unblocked", true);
		}
	}
}
