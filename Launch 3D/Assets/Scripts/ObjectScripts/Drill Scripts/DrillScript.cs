using UnityEngine;
using System.Collections;

public class DrillScript : MonoBehaviour {

	/*
	 * Script for the drill heads themselves
	 * */

	public AudioClip drillSound;

	public bool correctDrill = true;
	public GameObject rock;
	private RockScript rockScript;
	private float drillDuration = 0f;
	private float moveDuration = 0f;
	private bool drilling = false;
	public bool drilled = false;
	private bool down = false;
	private Vector3 step = new Vector3(0f, 1200f, 0f);

	void Start() {
		rockScript = rock.GetComponent<RockScript>();
		if(correctDrill) {
			gameObject.SetActive(false);
		}
	}

	void Update() {
		if(drillDuration > 0f) {
			Spin();
		} else {
			if(rockScript.onRock && drilling) {
				audio.Stop();
				drilled = true;
			}
		}
		if(moveDuration > 0f) {
			if(down) {
				MoveDown();
			} else {
				MoveUp();
			}
		}
	}

	private void Spin() {
		transform.Rotate(step * Time.deltaTime, Space.World);
		drillDuration -= Time.deltaTime;
	}

	private void MoveDown() {
		transform.Translate(step * Time.deltaTime, Space.World);
		moveDuration -= Time.deltaTime;
	}
	
	private void MoveUp() {
		//Both will need anchors
		transform.Translate(-step * Time.deltaTime, Space.World);
		//transform.position = Vector3.MoveTowards(transform.position, anchor, Time.deltaTime);
		moveDuration -= Time.deltaTime;
	}

	public void Move(float duration, bool down) {
		moveDuration = duration;
		this.down = down;
	}

	public void Drill(float duration) {
		drillDuration = duration;
		if (rockScript.onRock && correctDrill) {
			audio.Play ();
			drilling = true;
		}
		else {audio.PlayOneShot(drillSound);}
	}

}
