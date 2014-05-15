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
	public bool jammed;
	public int locks = 0;
	public bool airLocked;
	public AudioClip doorOpenSound;
	public AudioClip doorCloseSound;
	public AudioClip lowPowerSound;
	//public GameObject receptacle;

	void Start()
	{
		//Load saved door state
		PlayerPrefsX.GetBool(gameObject.name + "Locked", locked);
		PlayerPrefsX.GetBool(gameObject.name + "lowPower", lowPower);
		PlayerPrefs.GetInt(gameObject.name + "locks", locks);
		PlayerPrefsX.GetBool(gameObject.name + "airLocked", airLocked);

		doorAnimator.SetBool("Locked", locked);
		doorAnimator.SetBool("Jammed", jammed);
		doorAnimator.SetBool("lowPower", lowPower);
	}

	public void Save()
	{
		PlayerPrefsX.SetBool(gameObject.name + "Locked", locked);
		PlayerPrefsX.SetBool(gameObject.name + "lowPower", lowPower);
		PlayerPrefs.SetInt(gameObject.name + "locks", locks);
		PlayerPrefsX.SetBool(gameObject.name + "airLocked", airLocked);
	}

	public bool isJammed
	{
		get
		{
			jammed = doorAnimator.GetBool("Jammed");
			return locked;
		}
		set
		{
			//print (value);
			jammed = value;
			doorAnimator.SetBool("Jammed", jammed);
		}
	}

	public bool isLocked
	{
		get
		{
			locked = doorAnimator.GetBool("Locked");
			return locked;
		}
		set
		{
			//print (value);
			locked = value;
			if(!locked && !airLocked) {
				doorAnimator.SetBool("Locked", false);
			} else {
				doorAnimator.SetBool("Locked", true);
			}
		}
	}

	public bool isAirLocked
	{
		get
		{
			return airLocked;
		}
		set
		{
			airLocked = value;
			if(!locked && !airLocked) {
				doorAnimator.SetBool("Locked", false);
			} else {
				doorAnimator.SetBool("Locked", true);
			}
		}
	}

	
	private Animator anim;


	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			doorAnimator.SetBool("lowPower", lowPower);
			doorAnimator.SetBool("Open", false);
			if (!(locked || airLocked))
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
			if (!(locked || airLocked));
			{
				JournalScript.addItem("Door Opened");
				audio.Stop();
				if (!lowPower) audio.PlayOneShot(doorOpenSound);
				else audio.PlayOneShot(lowPowerSound);
			}
		}
	}
}
