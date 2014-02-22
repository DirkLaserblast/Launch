using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

//	public bool locked = false;
//	public bool open = false;
//	public AnimationClip openAnimation;
//	public AnimationClip closeAnimation;
//	public Animation anim;
	public Animator doorAnimator;
	public bool locked;
	public AudioClip doorOpenSound;
	public AudioClip doorCloseSound;


	public bool isLocked
	{
		get
		{
			locked = doorAnimator.GetBool("Locked");
			return locked;
		}
		set
		{
			locked = value;
			doorAnimator.SetBool("Locked", locked);
		}
	}
	

	private Animator anim;


	// Use this for initialization
	void Start () {
		doorAnimator.SetBool ("Locked", locked);
	}

	void OnTriggerExit (Collider other)
	{
		doorAnimator.SetBool("Open", false);
		audio.PlayOneShot(doorCloseSound);
	}

	void OnTriggerEnter (Collider other)
	{
		doorAnimator.SetBool("Open", true);
		if (!locked) audio.PlayOneShot(doorOpenSound);
	}
}
