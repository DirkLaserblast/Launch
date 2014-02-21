using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

//	public bool locked = false;
//	public bool open = false;
//	public AnimationClip openAnimation;
//	public AnimationClip closeAnimation;
//	public Animation anim;
	public Animator doorAnimator;
	private Animator anim;


	// Use this for initialization
	void Start () {

	}

	void OnTriggerExit (Collider other)
	{
		doorAnimator.SetBool("Open", false);
	}

	void OnTriggerEnter (Collider other)
	{
		doorAnimator.SetBool("Open", true);
	}
}
