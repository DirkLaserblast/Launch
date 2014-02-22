using UnityEngine;
using System.Collections;

public class DrillMove : MonoBehaviour {

	/*
	 * Script for the game object that is moved by the movement button.
	 *  */


	private float moveDuration = 0f;
	private bool down = false;
	private Vector3 step = new Vector3(0f, -0.04f, 0f);
	private Vector3 anchor;
	
	void Start() {
		anchor = transform.position;
	}

	void Update() {
		if (moveDuration > 0f) {
			if (down) {
				MoveDown();
			} else {
				MoveUp();
			}
		}
	}
	
	private void MoveDown() {
		transform.Translate(step * Time.deltaTime, Space.World);
		moveDuration -= Time.deltaTime;
	}
	
	private void MoveUp() {
		//transform.position = Vector3.MoveTowards(transform.position, anchor, Time.deltaTime);
		transform.Translate(-step * Time.deltaTime, Space.World);
		moveDuration -= Time.deltaTime;
		if(transform.position.y > anchor.y) {
			audio.Stop ();
			Stop();
		}
	}

	private void ChangeDirection() {
		if (down) {
			down = false;
		} else {
			down = true;
		}
	}

	public void Move(float duration) {
		if (moveDuration <= 0f) {
			moveDuration = duration;
			ChangeDirection(); //swap direction each time button is pressed
		}
	}

	public void InterruptMove(float duration) {
		audio.Play ();
		moveDuration = duration;
		ChangeDirection(); //swap direction each time button is pressed
	}

	public void Stop() {
		moveDuration = 0;
	}

}
