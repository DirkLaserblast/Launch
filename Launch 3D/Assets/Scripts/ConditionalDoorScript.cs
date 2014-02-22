using UnityEngine;
using System.Collections;

public class ConditionalDoorScript : MonoBehaviour {

	//	public bool locked = false;
	//	public bool open = false;
	//	public AnimationClip openAnimation;
	//	public AnimationClip closeAnimation;
	//	public Animation anim;
	public Animator doorAnimator;
	public bool locked;
	public GameObject drill;
	public GameObject vent;
	private DrillMachineScript DMS;
	private AirVentScript AVS;
	private Animator anim;
	
	
	// Use this for initialization
	void Start () {
		doorAnimator.SetBool ("Locked", locked);
		DMS = drill.GetComponent<DrillMachineScript> ();
		AVS = vent.GetComponent<AirVentScript> ();
	}
	
	void OnTriggerExit (Collider other)
	{
		doorAnimator.SetBool("Open", false);
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (AVS.completed && DMS.completed) {
			doorAnimator.SetBool ("Open", true);
		}
	}
}
