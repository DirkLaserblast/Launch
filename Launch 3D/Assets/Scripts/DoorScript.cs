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
	public bool lowPower;
	public AudioClip doorOpenSound;
	public AudioClip doorCloseSound;
	public AudioClip lowPowerSound;
	//public GameObject receptacle;
	
	public bool isLocked
	{
		get
		{
			locked = doorAnimator.GetBool("Locked");
			return locked;
		}
		set
		{
			print (value);
			locked = value;
			doorAnimator.SetBool("Locked", locked);
		}
	}
	
	
	private Animator anim;
	
	
	// Use this for initialization
	void Start () {
		doorAnimator.SetBool("Locked", locked);
		doorAnimator.SetBool("lowPower", lowPower);
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			doorAnimator.SetBool("lowPower", lowPower);
			doorAnimator.SetBool("Open", false);
			if (!locked)
			{
				audio.Stop();
				if (!lowPower) audio.PlayOneShot(doorCloseSound);
				else audio.PlayOneShot(lowPowerSound);
			}
		}
		
	}
	
	void OnTriggerEnter (Collider other)
	{
		
		if (other.tag == "Player")
		{
			doorAnimator.SetBool("lowPower", lowPower);
			doorAnimator.SetBool("Open", true);
			if (!locked)
			{
				audio.Stop();
				if (!lowPower) audio.PlayOneShot(doorOpenSound);
				else audio.PlayOneShot(lowPowerSound);
			}
		}
	}
}
