using UnityEngine;
using System.Collections;

//Creates a fade-through-black transition between scenes
public class TransitionAnimatorScript : MonoBehaviour {

	private float alphaFadeValue = 1.0f;
	public Texture blackTexture;
	private bool fadingOut;
	private bool fadingIn;
	private float transitionTime;

	// Use this for initialization
	void Start () {
	
	}

	void fadeOut(float seconds)
	{
		fadingOut = true;
		fadingIn = false;
		transitionTime = seconds;
	}

	void fadeIn(float seconds)
	{
		fadingIn = true;
		fadingOut = false;
		transitionTime = seconds;
	}

	// Update is called once per frame
	void Update ()
	{
		if (fadingOut) alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / transitionTime);
		if (fadingIn) alphaFadeValue += Mathf.Clamp01(Time.deltaTime / transitionTime);

		if (alphaFadeValue >= 0.0f | alphaFadeValue <= 1.0f)
		{
			fadingOut = false;
			fadingIn = false;
		}
	}

	void OnGUI ()
	{	
		if (fadingIn | fadingOut)
		{

		}
		GUI.color = new Color(0, 0, 0, alphaFadeValue);
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), blackTexture);
	}
}
