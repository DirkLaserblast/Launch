using UnityEngine;
using System.Collections;

public class PlayerToggle : MonoBehaviour {

	public GameObject player;
	private FirstPersonCharacter FPCscript;
	private SimpleMouseRotator SMRscript;
	private FirstPersonHeadBob FPHBscript;


	// Use this for initialization
	void Start () {
		FPCscript = player.GetComponent<FirstPersonCharacter> ();
		FPHBscript = player.GetComponent<FirstPersonHeadBob> ();
		SMRscript = player.GetComponent<SimpleMouseRotator> ();
	
	}

	public void DisableMouselook() {
		SMRscript.enabled = false;
		FPHBscript.enabled = false;
	}
	
	public void EnableMouselook() {
		SMRscript.enabled = true;
		FPHBscript.enabled = true;
	}
	
	public void DisableMovement() {
		FPCscript.enabled = false;
	}
	
	public void EnableMovement() {
		FPCscript.enabled = true;
	}
}
