using UnityEngine;
using System.Collections;

public class AsparagusLightSwitch : MonoBehaviour {
	
	public GameObject[] asparagusLights;
	public float maxIntensity;
	SliderScript sliderScript;

	// Use this for initialization
	void Start () {
		//asparagusLights = GameObject.FindGameObjectsWithTag("Asparagus Light");
		sliderScript = gameObject.GetComponent<SliderScript>();
		sliderScript.sliderValue = asparagusLights[0].light.intensity / maxIntensity;
	}
	
	// Update is called once per frame
	void Update () {
		if (sliderScript.guiOn)
		{
			foreach(GameObject aLight in asparagusLights)
			{
				aLight.light.intensity = maxIntensity * sliderScript.sliderValue/1.0f;
			}
		}
	}
}
