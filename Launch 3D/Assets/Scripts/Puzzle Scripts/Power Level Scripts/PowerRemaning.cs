using UnityEngine;
using System.Collections;

public class PowerRemaning : MonoBehaviour {

	private const string powerRemaining = "Power Remaining: ";
	private string currentPower;
	public PowerManager powerManager;
	public dfTextbox dfTextboxScript;

	void Update () {
		currentPower = "" + powerManager.CurrentPower;
		dfTextboxScript.Text = powerRemaining + currentPower;
	}
}
