using UnityEngine;
using System.Collections;

public class LightBlinkScript : MonoBehaviour {

	public bool blinking;
	public float fullPower = 1.0f;
	public float blinkRate = 1.0f;

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating("Blink",0.01f,1.0f);
	}

	void Blink()
	{
		if (blinking)
		{
			if (light.intensity < fullPower)
			{
				light.intensity = fullPower;
			}
			else light.intensity = fullPower/2;
		}
		else light.intensity = fullPower;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
